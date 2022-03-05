using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public class ErrorDataResult<T>:DataResult<T>
    {
        //tüm bilgileri verdiğimiz versiyon

        public ErrorDataResult(T data, string message) : base(data, false, message)
        {
        }

        //mesaj yok sadece data versiyonu ama  sonuc başarısız diyoruz
        public ErrorDataResult(T data) : base(data, false)
        {
        }

        //sadece mesaj için, data ise default birsey döndürmek istemeyebilirisin,ama herturlu sonuç başarısız 
        public ErrorDataResult(string message) : base(default, false, message)
        {

        }

        //hiçbirşey vermiyeceğiz ama herturlu sonuç başarısız ve default bir data var
        public ErrorDataResult() : base(default, false)
        {

        }
    }
}
