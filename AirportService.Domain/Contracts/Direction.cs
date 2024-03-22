using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportService.Domain.Contracts
{
    /// <summary>
    /// Направления сообщения
    /// </summary>
    public enum Direction
    {
        /// <summary>
        /// Исходящие
        /// </summary>
        [Description("Исходящие")]
        Out = 1, 
        
        /// <summary>
        /// Входящие
        /// </summary>
        [Description("Входящие")]
        In = 2
    }


    /// <summary>
    /// Класс расширений
    /// </summary>
    public static class DirectionExt
    {
        /// <summary>
        /// Описание текстом
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static string Name(this Direction direction)
        {
            switch (direction)
            {
                case Direction.Out:
                    return "исходящие";
                case Direction.In:
                    return "входящие";
                default:
                    throw new ArgumentException($"Неизвестный параметр направления сообщения {direction}");
            }
        }


        /// <summary>
        /// Описание глаголом
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static string Verb(this Direction direction)
        {
            switch (direction)
            {
                case Direction.Out:
                    return "отправил в";
                case Direction.In:
                    return "получил от";
                default:
                    throw new ArgumentException($"Неизвестный параметр направления сообщения {direction}");
            }
        }


        /// <summary>
        /// Описание символом
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static string Icon(this Direction direction)
        {
            switch (direction)
            {
                case Direction.Out:
                    return " => ";
                case Direction.In:
                    return " <= ";
                default:
                    throw new ArgumentException($"Неизвестный параметр направления сообщения {direction}");
            }
        }
    }
}
