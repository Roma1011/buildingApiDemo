using webapi.Models.Dto;

namespace webapi.Data
{
    public static  class WebapiStore
    {
        public static List<WebapiDTO>WebList=new List<WebapiDTO>
        {
            new WebapiDTO{Id=1,Name="pool view"},
            new WebapiDTO{Id=2,Name="ban view"}
        };
    }
}
