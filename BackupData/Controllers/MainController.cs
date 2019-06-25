using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackupData.Controllers
{
    public class MainController
    {
        //Tạo instance
        private static MainController instance;

        public static MainController Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MainController();                    
                }
                return instance;
            }
        }
        public int GoiHam(int a, int b)
        {
            return a + b;
        }

    }
}
