using preThi.Models;
using System.Data.Linq;
using System.Linq;
using preThi.Models;
public class UserSingleton
{
    private static UserSingleton instance;
    private static readonly object lockObject = new object();
    private DataClasses1DataContext db;

    private UserSingleton()
    {
        DataClasses1DataContext db = new DataClasses1DataContext(global::System.Configuration.ConfigurationManager.ConnectionStrings["demo_sqlsvConnectionString"].ConnectionString); // Replace with your DataContext instantiation logic
    }

    public static UserSingleton Instance
    {
        get
        {
            if (instance == null)
            {
                lock (lockObject)
                {
                    if (instance == null)
                    {
                        instance = new UserSingleton();
                    }
                }
            }

            return instance;
        }
    }

    public void SaveUser(tbl_NguoiDung user)
    {
        this.db.tbl_NguoiDungs.InsertOnSubmit(user);
        this.db.SubmitChanges();
    }
}
