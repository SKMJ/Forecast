using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Lawson.M3.MvxSock;

namespace WindowsFormsCoriqTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        //This function lists all fields in "CRS610MI"

        private void button1_Click(object sender, EventArgs e)
        {
            SERVER_ID server_id = new SERVER_ID();
            uint answer_int;
            char a = 'p';
            string d = "dhfjdfk";
            d = "s";
            uint rc = 0;

            try
            {


                rc = MvxSock.Connect(ref server_id, "172.31.157.25", 16205, "mi310", "MIPGM99", "CRS610MI", null);

                if(rc !=0)
                {
                    Console.WriteLine("Error: " + rc);
                }


                StringBuilder ret2 = new StringBuilder(1024);
                uint size2 = (uint)ret2.Capacity;

                String cmd2 = "SetLstMaxRec   999";
                try
                {
                    //MvxSock.Access(ref server_id, cmd);
                    //Trans needs input as one string not set fields
                    //MvxSock.Access can use setfield
                    MvxSock.Trans(ref server_id, cmd2, ret2, ref size2);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("List by number Error" + ex.Message);
                }


                StringBuilder ret = new StringBuilder(1024);
                uint size =(uint) ret.Capacity;

                String cmd = "LstByNumber";
                try
                {
                    //MvxSock.Access(ref server_id, cmd);
                    MvxSock.Trans(ref server_id, cmd, ret, ref size);
                }
                catch(Exception ex)
                {
                    Console.WriteLine("List by number Error" + ex.Message);
                }

                Console.WriteLine("result: " + ret.ToString());

                String temp;
                List<string> listCustomer = new List<string>();
                

                while(ret.ToString().StartsWith("REP"))
                {
                    temp = ret.ToString().Substring(0,150);
                    listCustomer.Add(temp);
                    MvxSock.Receive(ref server_id, ret, ref size);
                }
                    
                foreach(string customer in listCustomer)
                {
                    Console.WriteLine("list: " +   customer.ToString());
                }



                



                MvxSock.Close(ref server_id);




            }
            catch (Exception ex)
            {
                Console.WriteLine("Fel med connect!!: " + ex);
                //MvxSock.Close(ref server_id);
            }

            button2.Text = "Messi";
        }


        //This button lists special fields from input
        private void button3_Click(object sender, EventArgs e)
        {
            SERVER_ID sid = new SERVER_ID();

            uint rc;

            rc = MvxSock.Connect(ref sid, "172.31.157.25", 16205, "mi310", "MIPGM99", "CRS610MI", null);
            if (rc != 0)
            {
                MvxSock.ShowLastError(ref sid, "Error no " + rc + "\n");
                return;
            }

            StringBuilder ret2 = new StringBuilder(1024);
            uint size2 = (uint)ret2.Capacity;

            String cmd2 = "SetLstMaxRec   5";
            try
            {
                //MvxSock.Access(ref server_id, cmd);
                MvxSock.Trans(ref sid, cmd2, ret2, ref size2);
            }
            catch (Exception ex)
            {
                Console.WriteLine("List by number Error" + ex.Message);
            }

            //Set the field without need to know position Start from this customer 00752
            MvxSock.SetField(ref sid, "CONO", "001");
            MvxSock.SetField(ref sid, "CUNO", "00752");
            rc = MvxSock.Access(ref sid, "LstByNumber");
            if (rc != 0)
            {
                MvxSock.ShowLastError(ref sid, "Error no " + rc + "\n");
                MvxSock.Close(ref sid);
                return;
            }

            while (MvxSock.More(ref sid))
            {
                Console.Write(MvxSock.GetField(ref sid, "CUNO") + "\t\t");
                Console.WriteLine(MvxSock.GetField(ref sid, "CUNM"));
                //Mooves to next row
                MvxSock.Access(ref sid, null);
            }

            MvxSock.Close(ref sid);
        }


        //Change name
        private void button4_Click(object sender, EventArgs e)
        {
           
            SERVER_ID sid = new SERVER_ID();

            uint rc;

            rc = MvxSock.Connect(ref sid, "172.31.157.25", 16205, "mi310", "MIPGM99", "CRS610MI", null);
            if (rc != 0)
            {
                MvxSock.ShowLastError(ref sid, "Error no " + rc + "\n");
                return;
            }

            StringBuilder ret2 = new StringBuilder(1024);
            uint size2 = (uint)ret2.Capacity;

            String cmd2 = "SetLstMaxRec   5";
            try
            {
                //MvxSock.Access(ref server_id, cmd);
                MvxSock.Trans(ref sid, cmd2, ret2, ref size2);
            }
            catch (Exception ex)
            {
                Console.WriteLine("List by number Error" + ex.Message);
            }

            //Set the field without need to know position Start from this customer 00752
            MvxSock.SetField(ref sid, "CONO", "001"); //Nyckel//INPUT för att hitta CONO Alltid 1
            MvxSock.SetField(ref sid, "CUNO", "TESTJE"); //Nyckel//INPUT för att hitta //Customer number
            //MvxSock.SetField(ref sid, "CUNO", "APANSSON");
            MvxSock.SetField(ref sid, "CUNM", textBoxName.Text); //ändra till input
            rc = MvxSock.Access(ref sid, "ChgBasicData");
            if (rc != 0)
            {
                MvxSock.ShowLastError(ref sid, "Error no " + rc + "\n");
                MvxSock.Close(ref sid);
                return;
            }
            Console.WriteLine("Customer Number: " + MvxSock.GetField(ref sid, "CUNO"));



            MvxSock.Close(ref sid);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            SERVER_ID sid = new SERVER_ID();

            uint rc;

            rc = MvxSock.Connect(ref sid, "172.31.157.25", 16205, "mi310", "MIPGM99", "CRS105MI", null);
            if (rc != 0)
            {
                MvxSock.ShowLastError(ref sid, "Error no " + rc + "\n");
                return;
            }

            StringBuilder ret2 = new StringBuilder(1024);
            uint size2 = (uint)ret2.Capacity;

            String cmd2 = "SetLstMaxRec   5";
            try
            {
                //MvxSock.Access(ref server_id, cmd);
                MvxSock.Trans(ref sid, cmd2, ret2, ref size2);
            }
            catch (Exception ex)
            {
                Console.WriteLine("SetLstMaxRec error" + ex.Message);
            }

            //Set the field without need to know position Start from this customer 00752
            MvxSock.SetField(ref sid, "ASCD", "COOP");
            MvxSock.SetField(ref sid, "CONO", "001");
            rc = MvxSock.Access(ref sid, "LstAssmItem");
            if (rc != 0)
            {
                MvxSock.ShowLastError(ref sid, "Error in get products no " + rc + "\n");
                MvxSock.Close(ref sid);
                return;
            }

            while (MvxSock.More(ref sid))
            {
                Console.Write("Item nr: " + MvxSock.GetField(ref sid, "ITNO") + "\t\t");
                Console.WriteLine("Kedja: " + MvxSock.GetField(ref sid, "ASCD"));
                //Mooves to next row
                MvxSock.Access(ref sid, null);
            }

            MvxSock.Close(ref sid);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            GetFromM3 m3_communicator = new GetFromM3();


            List<int> tempList = m3_communicator.GetListOfProductsNbrByAssortment("COOP");

            foreach (int item in tempList)
            {
                string temp = m3_communicator.GetNameByItemNumber(item);
                Console.WriteLine("nr: " + item + "  Nametemp: " + temp);
            }
        }
    }
}
