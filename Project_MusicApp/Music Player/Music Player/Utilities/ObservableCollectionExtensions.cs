﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Music_Player.Utilities
{
    public static class ObservableCollectionExtensions
    {
        public static T NextItem<T>(this ObservableCollection<T> collection, T currentItem)
        {
            var currentIndex = collection.IndexOf(currentItem);
            if (currentIndex < collection.Count - 1)
            {
                return collection[currentIndex + 1];
            }
            return collection.FirstOrDefault();
        }
        public static T PreviousItem<T>(this ObservableCollection<T> collection, T currentItem)
        {
            var currentIndex = collection.IndexOf(currentItem);
            if (currentIndex > 0)
            {
                return collection[currentIndex- 1];
            }
            return collection.Last();
        }
        public static ObservableCollection<T> Shuffle<T>(this ObservableCollection<T> input)
        {
            var provider = new RNGCryptoServiceProvider();
            var n = input.Count;
            while (n > 1)
            {
                var box = new byte[1];
                do provider.GetBytes(box);
                while (!(box[0] < n * (Byte.MaxValue / n)));
                var k = (box[0] % n);
                n--;
                var value = input[k];
                input[k] = input[n];
                input[n] = value;
            }

            return input;
        }
    }

}
