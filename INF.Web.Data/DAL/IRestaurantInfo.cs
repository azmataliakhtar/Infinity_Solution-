using INF.Web.Data.Entities;

namespace INF.Web.Data.DAL
{
    public interface IRestaurantInfo
    {
        CsRestaurant GetRestaurantInfo(int id);
        CsRestaurant SaveRestaurantInfo(CsRestaurant res);
    }
}