namespace Alpha.MemberInfo.DataAccesses
{
    using Criteria;
    using Fanex.Common;
    using Fanex.Data;
    using Models;

    public interface CheckUMDataAccess
    {
        Result CheckUM(CheckUMCriteria criteria);
    }

    public class CheckUMDataAccessImpl : CheckUMDataAccess
    {
        public Result CheckUM(CheckUMCriteria criteria)
        {
            using (var db = ObjectDbFactory.CreateInstance(criteria.GetSettingKey()))
            {
                db.ExecuteNonQuery(criteria.GetSpParams());

                var outputParams = db.GetOuputParams();

                return new Result
                {
                    Code = Converter.ToInt(outputParams["errcode"]),
                    Message = Converter.To<string>(outputParams["errmsg"])
                };
            }
        }
    }
}