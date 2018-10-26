using System;
using UnityEngine;

namespace Maryan.HeroesOfCode
{
    [CreateAssetMenu(fileName = nameof(LeePathfinder), menuName = EditorUtils.GridSystem + nameof(LeePathfinder))]
    public class LeePathfinder : Pathfinder
    {
        private const int WALL = -1; // непроходимая ячейка
        private const int EMPTY = -2; // свободная непомеченная ячейка

        private int _width;// ширина рабочего поля
        private int _height;// высота рабочего поля

        private int[,] _grid;// рабочее поле
        private bool _isInitialize = false;

        public override void Initialize()
        {
            _width = Grid.Map.Width;
            _height = Grid.Map.Height;
            Path = new Point[_width * _height];
            _grid = new int[_height, _width];

            for(int y = 0; y < _height; y++)
            {
                for(int x = 0; x < _width; x++)
                {
                    _grid[y, x] = Grid.Map[x, y].Value - 2;
                }
            }
            _isInitialize = true;
        }

        //  перед вызовом поиска необходимо проинициализировать
        public override bool Find(Point start, Point end)
        {
            if(!_isInitialize)
            {
                Debug.Log("Pathfainder is not initialized!");
                return false;
            }
            if(start == end)
            {
                PathLength = 0;
                return false;
            }

            int[] dx = { 1, 0, -1, 0, -1, 1, 1, -1 };   // смещения, соответствующие соседям ячейки
            int[] dy = { 0, 1, 0, -1, -1, 1, -1, 1 };   // справа, снизу, слева и сверху и диагональные

            int d, x, y, k;
            bool stop;

            if(_grid[start.y, start.x] == WALL || _grid[end.y, end.x] == WALL)
            {
                return false;
            }

            // распространение волны
            d = 0;
            _grid[start.y, start.x] = 0;// стартовая ячейка помечена 0
            do
            {
                stop = true;// предполагаем, что все свободные клетки уже помечены
                for(y = 0; y < _height; ++y)
                    for(x = 0; x < _width; ++x)
                        if(_grid[y, x] == d)// ячейка (x, y) помечена числом d
                        {
                            for(k = 0; k < dx.Length; ++k)// проходим по всем непомеченным соседям
                            {
                                int iy = y + dy[k], ix = x + dx[k];
                                if(iy >= 0 && iy < _height && ix >= 0 && ix < _width && _grid[iy, ix] == EMPTY)
                                {
                                    stop = false;// найдены непомеченные клетки
                                    _grid[iy, ix] = d + 1;// распространяем волну
                                }
                            }
                        }
                d++;
            } while(!stop && _grid[end.y, end.x] == EMPTY);

            if(_grid[end.y, end.x] == EMPTY)
            {
                return false; // путь не найден
            }

            // восстановление пути
            PathLength = _grid[end.y, end.x];// длина кратчайшего пути
            x = end.x;
            y = end.y;
            d = PathLength;

            while(d > 0)
            {
                Path[d].x = x;
                Path[d].y = y;// записываем ячейку (x, y) в путь
                d--;
                for(k = 0; k < dx.Length; ++k)
                {
                    int iy = y + dy[k], ix = x + dx[k];
                    if(iy >= 0 && iy < _height && ix >= 0 && ix < _width && _grid[iy, ix] == d)
                    {
                        x = x + dx[k];
                        y = y + dy[k];// переходим в ячейку, которая на 1 ближе к старту
                        break;
                    }
                }
            }
            Path = SubArray<Point>(Path, 0, PathLength + 1);
            Path[0].x = start.x;
            Path[0].y = start.y;
            return true;
        }

        private T[] SubArray<T>(T[] data, int index, int length)
        {
            T[] result = new T[length];
            Array.Copy(data, index, result, 0, length);
            return result;
        }
    }
}