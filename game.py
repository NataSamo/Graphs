import pygame as pg
from random import random, choice
from collections import deque


def get_rect(x, y):
    return x * TILE + 1, y * TILE + 1, TILE - 2, TILE - 2


def get_next_nodes(x, y):
    check_next_node = lambda x, y: True if 0 <= x < cols and 0 <= y < rows and not grid[y][x] else False
    ways = [-1, 0], [0, -1], [1, 0], [0, 1]
    return [(x + dx, y + dy) for dx, dy in ways if check_next_node(x + dx, y + dy)]


cols, rows = 15, 10
TILE = 50


class Cell:
    def __init__(self, x, y):
        self.x, self.y = x, y
        self.walls = {'top': True, 'right': True, 'bottom': True, 'left': True}
        self.visited = False

    def draw_current_cell(self):
        x, y = self.x * TILE, self.y * TILE
        pg.draw.rect(sc, pg.Color('turquoise'), (x + 2, y + 2, TILE - 2, TILE - 2))
    def draw_goal_cell(self):
        x, y = self.x * TILE, self.y * TILE
        pg.draw.rect(sc, pg.Color('black'), (x + 2, y + 2, TILE - 2, TILE - 2))

    def draw(self):
        x, y = self.x * TILE, self.y * TILE
        if self.visited:
            pg.draw.rect(sc, pg.Color('yellow'), (x, y, TILE, TILE))

        if self.walls['top']:
            pg.draw.line(sc, pg.Color('deeppink'), (x, y), (x + TILE, y), 3)
        if self.walls['right']:
            pg.draw.line(sc, pg.Color('deeppink'), (x + TILE, y), (x + TILE, y + TILE), 3)
        if self.walls['bottom']:
            pg.draw.line(sc, pg.Color('deeppink'), (x + TILE, y + TILE), (x , y + TILE), 3)
        if self.walls['left']:
            pg.draw.line(sc, pg.Color('deeppink'), (x, y + TILE), (x, y), 3)

    def check_cell(self, x, y):
        #find_index = lambda x, y: x + y * cols
        if x < 0 or x > cols - 1 or y < 0 or y > rows - 1:
            return False
        return grid[x][y]

    def check_neighbors(self):
        neighbors = []
        top = self.check_cell(self.x, self.y - 1)
        right = self.check_cell(self.x + 1, self.y)
        bottom = self.check_cell(self.x, self.y + 1)
        left = self.check_cell(self.x - 1, self.y)
        if top and not top.visited:
            neighbors.append(top)
        if right and not right.visited:
            neighbors.append(right)
        if bottom and not bottom.visited:
            neighbors.append(bottom)
        if left and not left.visited:
            neighbors.append(left)
        return choice(neighbors) if neighbors else False


    def step_right(self):
        if self.walls['right']:
            return self
        else:
            new_cell = Cell(self.x + 1, self.y)
            new_cell.walls = grid[new_cell.x][new_cell.y].walls
            #self.x = self.x + 1
            return new_cell



    def step_left(self):
        if self.walls['left']:
            return self
        else:
            new_cell = Cell(self.x - 1, self.y)
            new_cell.walls = grid[new_cell.x][new_cell.y].walls
            return new_cell



    def step_top(self):
        if self.walls['top']:
            return self
        else:
            new_cell = Cell(self.x, self.y - 1)
            new_cell.walls = grid[new_cell.x][new_cell.y].walls
            return new_cell



    def step_bottom(self):
        if self.walls['bottom']:
            return self
        else:
            new_cell = Cell(self.x, self.y + 1)
            new_cell.walls = grid[new_cell.x][new_cell.y].walls
            return new_cell


    def erase_current_cell(self):
        x, y = self.x * TILE, self.y * TILE
        pg.draw.rect(sc, pg.Color('yellow'), (x + 2, y + 2, TILE - 2, TILE - 2))









def remove_walls(current, next):
    dx = current.x - next.x
    if dx == 1:
        current.walls['left'] = False
        next.walls['right'] = False
    elif dx == -1:
        current.walls['right'] = False
        next.walls['left'] = False
    dy = current.y - next.y
    if dy == 1:
        current.walls['top'] = False
        next.walls['bottom'] = False
    elif dy == -1:
        current.walls['bottom'] = False
        next.walls['top'] = False

def get_click_mouse_pos():
    x, y = pg.mouse.get_pos()
    grid_x, grid_y = x // TILE, y // TILE
    pg.draw.rect(sc, pg.Color('red'), get_rect(grid_x, grid_y))
    click = pg.mouse.get_pressed()
    return (grid_x, grid_y) if click[0] else False


pg.init()
sc = pg.display.set_mode([cols * TILE, rows * TILE])
clock = pg.time.Clock()
# grid
#grid = [[1 if random() < 0.2 else 0 for col in range(cols)] for row in range(rows)]
grid = [[Cell(col, row)for row in range(rows)] for col in range(cols)]
# dict of adjacency lists

graph = {}
for y, row in enumerate(grid):
    for x, col in enumerate(row):
        if not col:
            graph[(x, y)] = graph.get((x, y), []) + get_next_nodes(x, y)


# BFS settings
start = (0, 0)
queue = deque([start])
visited = {start: None}
cur_node = start
current_cell = grid[0][0]
stack = []
colors, color = [], 255
sc.fill(pg.Color('purple'))
[[cell.draw() for cell in row] for row in grid]
current_cell.visited = True
current_cell.draw_current_cell()
    # [pygame.draw.rect(sc, colors[i], (cell.x * TILE + 2, cell.y * TILE + 2,
    # TILE - 4, TILE - 4)) for i, cell in enumerate(stack)]

next_cell = current_cell.check_neighbors()
if next_cell:
    next_cell.visited = True
    stack.append(current_cell)
        # colors.append((min(color, 255), 20, 147))
        # color += 1
    remove_walls(current_cell, next_cell)
    current_cell = next_cell
elif stack:
    current_cell = stack.pop()
while stack:
    [[cell.draw() for cell in row] for row in grid]
    current_cell.visited = True
    current_cell.draw_current_cell()
    # [pygame.draw.rect(sc, colors[i], (cell.x * TILE + 2, cell.y * TILE + 2,
    # TILE - 4, TILE - 4)) for i, cell in enumerate(stack)]

    next_cell = current_cell.check_neighbors()
    if next_cell:
        next_cell.visited = True
        stack.append(current_cell)
        # colors.append((min(color, 255), 20, 147))
        # color += 1
        remove_walls(current_cell, next_cell)
        current_cell = next_cell
    elif stack:
        current_cell = stack.pop()
    pg.display.flip()
    clock.tick(100)
[[cell.draw() for cell in row] for row in grid]
current_cell.visited = True
current_cell.draw_current_cell()
    # [pygame.draw.rect(sc, colors[i], (cell.x * TILE + 2, cell.y * TILE + 2,
    # TILE - 4, TILE - 4)) for i, cell in enumerate(stack)]

next_cell = current_cell.check_neighbors()
if next_cell:
    next_cell.visited = True
    stack.append(current_cell)
        # colors.append((min(color, 255), 20, 147))
        # color += 1
    remove_walls(current_cell, next_cell)
    current_cell = next_cell
elif stack:
    current_cell = stack.pop()
pg.display.flip()
clock.tick(100)

def bfs(start, goal):
    visited = set()

    q = []
    q.append(start)

    #visited.add(grid[0][0])
    parent = {}
    while q:
        cur_cell = q.pop(0)

        visited.add(cur_cell)

        if cur_cell == goal:
            break
        #print(cur_cell.walls)
        for k, v in cur_cell.walls.items():
            if k == 'top' and not v:

                next_cell = grid[cur_cell.x][cur_cell.y - 1]
                if next_cell not in visited:
                    q.append(next_cell)
                    parent[next_cell] = cur_cell
                '''
                next_cell = grid[cur_cell.x][cur_cell.y - 1]
                if next_cell not in visited:
                    q.append(next_cell)
                    s = []
                    s.append(next_cell.x)
                    s.append(next_cell.y)
                    st = ''.join(map(str, s))
                    s1 = []
                    s1.append(cur_cell.x)
                    s1.append(cur_cell.y)
                    st1 = ''.join(map(str, s1))
                    parent[st] = st1
                '''


            if k == 'bottom' and not v:

                next_cell = grid[cur_cell.x][cur_cell.y + 1]
                if next_cell not in visited:
                    q.append(next_cell)
                    parent[next_cell] = cur_cell
                '''
                next_cell = grid[cur_cell.x][cur_cell.y + 1]
                if next_cell not in visited:
                    q.append(next_cell)
                    s = []
                    s.append(next_cell.x)
                    s.append(next_cell.y)
                    st = ''.join(map(str, s))
                    s1 = []
                    s1.append(cur_cell.x)
                    s1.append(cur_cell.y)
                    st1 = ''.join(map(str, s1))
                    parent[st] = st1
                '''

            if k == 'left' and not v:

                next_cell = grid[cur_cell.x - 1][cur_cell.y]
                if next_cell not in visited:
                    q.append(next_cell)
                    parent[next_cell] = cur_cell
                '''
                next_cell = grid[cur_cell.x - 1][cur_cell.y]
                if next_cell not in visited:
                    q.append(next_cell)
                    s = []
                    s.append(next_cell.x)
                    s.append(next_cell.y)
                    st = ''.join(map(str, s))
                    s1 = []
                    s1.append(cur_cell.x)
                    s1.append(cur_cell.y)
                    st1 = ''.join(map(str, s1))
                    parent[st] = st1
                '''

            if k == 'right' and not v:

                next_cell = grid[cur_cell.x + 1][cur_cell.y]
                if next_cell not in visited:
                    q.append(next_cell)
                    parent[next_cell] = cur_cell
                '''

                next_cell = grid[cur_cell.x + 1][cur_cell.y]
                if next_cell not in visited:
                    q.append(next_cell)
                    s = []
                    s.append(next_cell.x)
                    s.append(next_cell.y)
                    st = ''.join(map(str, s))
                    s1 = []
                    s1.append(cur_cell.x)
                    s1.append(cur_cell.y)
                    st1 = ''.join(map(str, s1))
                    parent[st] = st1
                '''


    return parent







done = False
while not done:
    # fill screen
    #sc.fill(pg.Color('purple'))
    # draw grid
    #[[pg.draw.rect(sc, pg.Color('black'), get_rect(x, y), border_radius=TILE // 5)
      #for x, col in enumerate(row) if col] for y, row in enumerate(grid)]
    #current_cell = grid[0][0]
    #current_cell.draw_current_cell()
    #mouse_pos = get_click_mouse_pos()
    goal = grid[14][9]
    goal.draw_goal_cell()
    for event in pg.event.get():
        if event.type == pg.QUIT:
            done = True

        elif event.type == pg.KEYDOWN:
            #print(current_cell.walls)
            if event.key == pg.K_RIGHT:
                new_cell = current_cell.step_right()
                current_cell.erase_current_cell()
                current_cell = new_cell
                #current_cell.draw_current_cell()
            if event.key == pg.K_LEFT:
                new_cell = current_cell.step_left()
                current_cell.erase_current_cell()
                current_cell = new_cell
                #current_cell.draw_current_cell()
            if event.key == pg.K_UP:
                new_cell = current_cell.step_top()
                current_cell.erase_current_cell()
                current_cell = new_cell
                #current_cell.draw_current_cell()
            if event.key == pg.K_DOWN:
                new_cell = current_cell.step_bottom()
                current_cell.erase_current_cell()
                current_cell = new_cell
                #current_cell.draw_current_cell()
            if event.key == pg.K_1:
                parent = bfs(grid[0][0], goal)

                c = grid[0][0]
                g = goal
                a = g

                while g != c:
                   g.draw_goal_cell()
                   
                   g = parent[g]
                   '''
                   s = []
                   s.append(g.x)
                   s.append(g.y)
                   st = ''.join(map(str, s))
                   st1 = parent[st]
                   
                   g = grid[int(st1[0])][int(st1[1])]
                   '''






        elif event.type == pg.KEYUP:
            if event.key == pg.K_RIGHT:
                #current_cell.step_right()
                #current_cell.erase_current_cell()
                current_cell.draw_current_cell()
            if event.key == pg.K_LEFT:
                #current_cell.step_LEFT()
                #current_cell.erase_current_cell()
                current_cell.draw_current_cell()
            if event.key == pg.K_UP:
                #current_cell.step_top()
                #current_cell.erase_current_cell()
                current_cell.draw_current_cell()
            if event.key == pg.K_DOWN:
                #current_cell.step_bottom()
                #current_cell.erase_current_cell()
                current_cell.draw_current_cell()

    '''
    [[cell.draw() for cell in row] for row in grid]
    current_cell.visited = True
    current_cell.draw_current_cell()
    # [pygame.draw.rect(sc, colors[i], (cell.x * TILE + 2, cell.y * TILE + 2,
    # TILE - 4, TILE - 4)) for i, cell in enumerate(stack)]

    next_cell = current_cell.check_neighbors()
    if next_cell:
        next_cell.visited = True
        stack.append(current_cell)
        # colors.append((min(color, 255), 20, 147))
        # color += 1
        remove_walls(current_cell, next_cell)
        current_cell = next_cell
    elif stack:
        current_cell = stack.pop()
'''
    pg.display.flip()
    clock.tick(7)

