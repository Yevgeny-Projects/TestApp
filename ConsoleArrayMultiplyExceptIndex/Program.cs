using System;
using System.Collections.Generic;

namespace ConsoleArrayMultiplyExceptIndex
{
    class Program
    {
        static void Main(string[] args)
        {
            var arr = new int[]
            {
                2,6,7,8,3
            };

            var output = get_products_of_all_ints_except_at_index(arr);



            var num = getKthMgicNumber(10);
            Console.WriteLine(num);
        }


        static int[] get_products_of_all_ints_except_at_index(int[] int_list)
        {

            // создаем дополнительный массив с таким же размером, что и исходный.
            var products_of_all_ints_except_at_index = new int[int_list.Length];

            // Находим произведение всех значений до текущего.
            // Результат помещаем в новый массив.
            int product_so_far = 1;
            int i = 0;
            while (i < int_list.Length)
            {
                products_of_all_ints_except_at_index[i] = product_so_far;
                product_so_far *= int_list[i];
                i += 1;
            }

            // Находим произведение всех значений после текущего,
            // при этом двигаясь по массиву в обратную сторону.
            // Параллельно вычисляем значение текущей ячейки.
            product_so_far = 1;
            i = int_list.Length - 1;
            while (i >= 0)
            {
                products_of_all_ints_except_at_index[i] *= product_so_far;
                product_so_far *= int_list[i];
                i -= 1;
            }

            return products_of_all_ints_except_at_index;
        }


        public static int getKthMgicNumber(int k)
        {
            if (k < 0)
            {
                return 0;
            }
            int val = 0;
            Queue<int> queue3 = new Queue<int>();
            Queue<int> queue5 = new Queue<int>();
            Queue<int> queue7 = new Queue<int>();
            queue3.Enqueue(1);

            /* Итерация от 0 до k */
            for (int i = 0; i <= k; i++)
            {
                int v3 = queue3.Count > 0 ? queue3.Peek() :
                Int32.MaxValue;
                int v5 = queue5.Count > 0 ? queue5.Peek() :
                Int32.MaxValue;
                int v7 = queue7.Count > 0 ? queue7.Peek() :
                Int32.MaxValue;
                val = Math.Min(v3, Math.Min(v5, v7));
                if (val == v3)
                { // ставим в очередь 3, 5 и 7
                    queue3.Dequeue();
                    queue3.Enqueue(3 * val);
                    queue5.Enqueue(5 * val);
                }
                else if (val == v5)
                { // ставим в очередь 5 и 7
                    queue5.Dequeue();
                    queue5.Enqueue(5 * val);
                }
                else if (val == v7)
                { // ставим в очередь Q7
                    queue7.Dequeue();
                }
                queue7.Enqueue(7 * val); // всегда добавляем в очередь Q7
            }
            return val;
        }
    }
}
