using System;
using System.Collections.Generic;
using System.Text;

namespace Core.CrossCuttingConcerns.Caching
{
    public interface ICacheManager
    {
        //cache getirme
        T Get<T>(string key);
        //cache getirme
        object Get(string key);
        //cache ekleme
        void Add(string key, object data, int duration);
        // cache de var mı
        bool IsAdd(string key);
        //cacheden silme
        void Remove(string key);
        // içinde şu olanları sil gibi
        void RemoveByPattern(string pattern);
    }
}
