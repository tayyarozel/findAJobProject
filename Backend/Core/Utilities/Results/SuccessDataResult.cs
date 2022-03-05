using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public class SuccessDataResult<T>:DataResult<T>
    {
        //tüm bilgileri verdiğimiz versiyon
        public SuccessDataResult(T data, string message) : base(data, true, message)
        {
        }

        //mesaj yok sadece data versiyonu ama  sonuc başarılı diyoruz
        public SuccessDataResult(T data) : base(data, true)
        {
        }

        //sadece mesaj için, data ise default birsey döndürmek istemeyebilirisin,ama herturlu sonuç olumlu 
        public SuccessDataResult(string message) : base(default, true, message)
        {

        }

        //hiçbirşey vermiyeceğiz ama herturlu sonuç olumlu ve default bir data var
        public SuccessDataResult() : base(default, true)
        {

        }
    }
}
