﻿using System.Collections.Generic;

namespace Common
{
    public static class LinkedListExtensions
    {
        public static LinkedListNode<T> NextOrFirst<T>(this LinkedListNode<T> current)
        {
            return current.Next ?? current.List.First;
        }

        public static LinkedListNode<T> PreviousOrLast<T>(this LinkedListNode<T> current)
        {
            return current.Previous ?? current.List.Last;
        }
       
    }
}
