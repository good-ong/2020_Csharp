<<<<<<< HEAD
﻿using NHibernate;
using System;
using System.Collections.Generic;
=======
﻿using MySql.Data.MySqlClient;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Controls;
>>>>>>> a76ff0db92637bfa9420d9fe28bdbc9f5d5e002b
using System.Windows.Forms;

namespace _20200206_nhibernate_gyudong
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void loadstudentData()
        {
            ISession session = SessionFactory.OpenSession;

            using (session)
            {
                IQuery query = session.CreateQuery("From student");
                IList<Model.student> stuInfo = query.List<Model.student>();
                Model.student item = new Model.student();
                stuInfo.Add(item);
                dataGridView1.DataSource = stuInfo;
            }
        }
        
        private void SetstudentInfo(Model.student stu)
        {
            stu.grade = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
            stu.cclass = int.Parse(dataGridView1.CurrentRow.Cells[1].Value.ToString());
            stu.no = int.Parse(dataGridView1.CurrentRow.Cells[2].Value.ToString());
            stu.name = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            stu.score = dataGridView1.CurrentRow.Cells[4].Value.ToString();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            loadstudentData();

        }
        private void button1_Click(object sender, EventArgs e)
        {
            loadstudentData();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Model.student stuData = new Model.student();
            SetstudentInfo(stuData);
            ISession session = SessionFactory.OpenSession;
            using (session)
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        session.Save(stuData);
                        transaction.Commit();
                        loadstudentData();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show(ex.Message);
                        throw ex;
                    }
                }
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            Model.student stuData = new Model.student();
            SetstudentInfo(stuData);
            ISession session = SessionFactory.OpenSession;
            using (session)
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        session.Update(stuData);
                        transaction.Commit();
                        loadstudentData();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show(ex.Message);
                        throw ex;
                    }
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Model.student stuData = new Model.student();
            SetstudentInfo(stuData);
            ISession session = SessionFactory.OpenSession;
            using (session)
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        session.Delete(stuData);
                        transaction.Commit();
                        loadstudentData();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show(ex.Message);
                        throw ex;
                    }
                }
            }
        }
    }
}
