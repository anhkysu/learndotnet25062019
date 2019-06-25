using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BackupData.Controllers;

namespace BackupData
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Sự kiện lưu vào đây, không viết code trong này
            //Viết code trong controller
            //Link đến code đó bằng cách using BackupData.Controllers;
            //Đã viết ở trên rồi.
            //Gọi như sau:
            int temp =  MainController.Instance.GoiHam(1, 1);

            //Các struct tạo ra sử dụng chung cho toàn bộ code hoặc tạo ra để lưu trữ dữ liệu trong database được lưu trong Models
            //Anh có tạo một ví dụ trong  model.


            //Các struct tạo ra chỉ để phục vụ hàm controller thì tạo trong Viewmodel.
            //Mỗi struct tạo ra 1 class khác nhau, không tạo nhiều class trong một file.
            

            //View để tạo các giao diện trong này



        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
