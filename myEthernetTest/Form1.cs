﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace myEthernetTest
{
    public partial class Form1 : Form
    {
        DatabaseMgr DBMgr = new DatabaseMgr();
        ICDPacketMgr IcdMgr = new ICDPacketMgr();
        int clientID = -1;
        public Form1()
        {
            InitializeComponent();

            //Lambda express
            IcdMgr.OnRecv += (s, e) =>
            {
                ICD.HEADER obj = e as ICD.HEADER;
                MessageHandler(s, obj);
            };

            IcdMgr.StartServiceServer();

            DatabaseMgr.Open();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Console.WriteLine("onBtn~!!");
            clientID = IcdMgr.StartServiceClient("127.0.0.1", 7000);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ICD.HEADER head = new ICD.HEADER();
            head.id = (uint)ICD.COMMAND.NewUser;
            head.size = (uint)ICD.HEADER.HeaderSize();
            head.sof = (uint)ICD.MAGIC.SOF;
            head.type = 123;
            byte[] buf = ICD.HEADER.Serialize(head);
            NetworkMgr.GetInst().WriteToClient(clientID, buf);
        }

        private void MessageHandler(int clientID, ICD.HEADER obj)
        {
            var id = obj.id;
            switch(id)
            {
                case 0:
                    ICD_NewUser(obj);
                    break;
                case 1:
                    ICD_Login(clientID, obj);
                    break;
                case 2:
                    ICD_Logout(obj);
                    break;
                default:
                    break;
            }
        }

        private void ICD_NewUser(ICD.HEADER obj)
        {
            ICD.User msg = obj as ICD.User;

            if(DatabaseMgr.IsHaveUser(msg.userID))
            {
                //send back error msg : same user id
            }
            else
            {
                //push db new user
                //ack good
            }
        }
        private void ICD_Login(int clientID, ICD.HEADER obj)
        {
            ICD.User msg = obj as ICD.User;

            if (DatabaseMgr.IsHaveUser(msg.userID))
            {
                //if(isOKpassword)
                {
                    //loginUser();
                    //ack good
                }
                //else
                {
                    //send back error msg : wrong password
                }
            }
            else
            {
                //send back error msg : no user id
            }

        }
        private void ICD_Logout(ICD.HEADER obj)
        {

        }
    }
}
