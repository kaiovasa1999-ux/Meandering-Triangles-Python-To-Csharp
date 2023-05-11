def elevation_function(x,y):
    return 1/(2+math.sin(2*math.sqrt(x*x + y*y))) * (.75+.5*math.sin(x*2))

elevation_data = dict()
WIDTH, HEIGHT = 100, 100
SPACING = 1
for x in range(0,width, SPACING):
    for y in range(0,height, SPACING):
        elevation_data[(x,y)] = elevation_function(x,y)
        
import collections
Triangle = collections.namedtuple("Triangle", "v1 v2 v3")
triangles = []
for x in range(0,width-1, SPACING):
    for y in range(0,height-1, SPACING):
        t1 = Triangle((x,y), (x+SPACING,y), (x,y+SPACING))
        triangles.append(t1)
        t2 = Triangle((x+SPACING,y), (x,y+SPACING), (x+SPACING,y+SPACING))
        triangles.append(t2)
        
        
threshold = 0.5
Edge = collections.namedtuple("Edge", "e1 e2")
contour_segments = []

for triangle in triangles:
    below = [v for v in triangle if elevation_data[v] < threshold]
    above = [v for v in triangle if elevation_data[v] >= threshold]
    # All above or all below means no contour line here
    if len(below) == 0 or len(above) == 0:
        continue
    # We have a contour line, let's find it
    minority = above if len(above) < len(below) else below
    majority = above if len(above) > len(below) else below

    contour_points = []
    crossed_edges = (Edge(minority[0], majority[0]),
                     Edge(minority[0], majority[1]))
    for triangle_edge in crossed_edges:
        # how_far is a number between 0 and 1 indicating what percent
        # of the way along the edge (e1,e2) the crossing point is
        e1, e2 = triangle_edge.e1, triangle_edge.e2
        how_far = ((threshold - elevation_data[e2])
                   / (elevation_data[e1] - elevation_data[e2]))
        crossing_point = (
            how_far * e1[0] + (1-how_far) * e2[0],
            how_far * e1[1] + (1-how_far) * e2[1])
        contour_points.append(crossing_point)

    contour_segments.append(Edge(contour_points[0], contour_points[1]))
    unused_segments = set(contour_segments)
    
segments_by_point = collections.defaultdict(set)

for segment in contour_segments:
    segments_by_point[segment.e1].add(segment)
    segments_by_point[segment.e2].add(segment)

contour_lines = []
while unused_segments:
    # Start with a random segment
    line = collections.deque(unused_segments.pop())
    while True:
        tail_candidates = segments_by_point[line[-1]] & unused_segments
        if tail_candidates:
            tail = tail_candidates.pop()
            line.append(tail.e1 if tail.e2 == line[-1] else tail.e2)
            unused_segments.remove(tail)
        head_candidates = segments_by_point[line[0]] & unused_segments
        if head_candidates:
            head = head_candidates.pop()
            line.appendleft(head.e1 if head.e2 == line[0] else head.e2)
            unused_segments.remove(head)
        if not tail_candidates and not head_candidates:
            # There are no more segments touching this line,
            # so we're done with it.
            contour_lines.append(list(line))
            break
