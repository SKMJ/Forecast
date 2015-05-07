using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lawson.M3.MvxSock;

namespace WindowsFormsCoriqTest
{
    public class GetFromM3
    {
        Dictionary<int, string> productName = new Dictionary<int, string>();

        public List<int> GetListOfProductsNbrByAssortment(string Assortment)
        {
            List<int> productList = new List<int>();

            {

                SERVER_ID sid = new SERVER_ID();

                uint rc;

                rc = MvxSock.Connect(ref sid, "172.31.157.25", 16205, "mi310", "MIPGM99", "CRS105MI", null);
                if (rc != 0)
                {
                    MvxSock.ShowLastError(ref sid, "Error no " + rc + "\n");
                    return null;
                }

                //Set the field without need to know position Start from this customer 00752
                MvxSock.SetField(ref sid, "ASCD", Assortment);
                MvxSock.SetField(ref sid, "CONO", "001");
                rc = MvxSock.Access(ref sid, "LstAssmItem");
                if (rc != 0)
                {
                    MvxSock.ShowLastError(ref sid, "Error in get products no " + rc + "\n");
                    MvxSock.Close(ref sid);
                    return null;
                }

                while (MvxSock.More(ref sid))
                {
                    string tempItemNBR = MvxSock.GetField(ref sid, "ITNO") + "\t\t";
                    Console.Write("Item nr: " + tempItemNBR);
                    Console.WriteLine("Kedja: " + MvxSock.GetField(ref sid, "ASCD"));
                    //Mooves to next row
                    MvxSock.Access(ref sid, null);

                    productList.Add(Convert.ToInt32(tempItemNBR));
                }

                MvxSock.Close(ref sid);
                return productList;
            }
        }




        public string GetNameByItemNumber(int itemNbr)
        {
            string returnString = "";
            List<int> productList = new List<int>();

            {

                SERVER_ID sid = new SERVER_ID();

                uint rc;

                rc = MvxSock.Connect(ref sid, "172.31.157.25", 16205, "mi310", "MIPGM99", "MMS200MI", null);
                if (rc != 0)
                {
                    MvxSock.ShowLastError(ref sid, "Error no " + rc + "\n");
                    return returnString;
                }

                //Set the field without need to know position Start from this customer 00752
                MvxSock.SetField(ref sid, "ITNO", itemNbr.ToString());
                MvxSock.SetField(ref sid, "CONO", "001");
                rc = MvxSock.Access(ref sid, "GetItmBasic");
                if (rc != 0)
                {
                    MvxSock.ShowLastError(ref sid, "Error in get products no " + rc + "\n");
                    MvxSock.Close(ref sid);
                    return returnString;
                }

                string tempItemNBR = MvxSock.GetField(ref sid, "ITNO") + "\t\t";
                returnString = MvxSock.GetField(ref sid, "ITDS");

                //while (MvxSock.More(ref sid))
                //{
                //    //string tempItemNBR = MvxSock.GetField(ref sid, "ITNO") + "\t\t";
                //    //returnString = MvxSock.GetField(ref sid, "ITDS");
                //    Console.Write("Item nr: " + tempItemNBR);
                //    Console.WriteLine("Namn på produkten: " + MvxSock.GetField(ref sid, returnString));
                //    //Mooves to next row
                //    MvxSock.Access(ref sid, null);
                //}

                MvxSock.Close(ref sid);
                return returnString;
            }
        }
        
    }
}
