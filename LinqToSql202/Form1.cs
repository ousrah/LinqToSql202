using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LinqToSql202
{
    public partial class Form1 : Form
    {
        librairieDataContext db = new librairieDataContext();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
               var ed = db.EDITEUR.Where(c => c.VILLEED=="paris" );
         //   var ed = from edi in db.EDITEUR
         //            where edi.VILLEED == "paris"
         //            select edi;

            label1.Text = ed.Count().ToString();




            dataGridView1.DataSource = ed;

        }

        private void button3_Click(object sender, EventArgs e)
        {

         
            //afficher la liste des noms et des villes de tous les editeurs
            var ed = from edi in db.EDITEUR
                     select new { nom = edi.NOMED, ville = edi.VILLEED };

            dataGridView1.DataSource = ed;
        }

        private void button4_Click(object sender, EventArgs e)
        {



            //afficher la liste des noms et des villes des editeurs qui habitent a paris

            //avec ==
            //var ed = from edi in db.EDITEUR
            //         where edi.VILLEED == "paris"
            //         select new { edi.NOMED, edi.VILLEED };

            //avec equals
            var ed = from edi in db.EDITEUR
                     where edi.VILLEED.Equals("PARIS")
                     select new { edi.NOMED, edi.VILLEED };


            dataGridView1.DataSource = ed;
        }

        private void button5_Click(object sender, EventArgs e)
        {



            //afficher la liste des noms et des villes des editeurs qui habitent a paris
            //avec le tri descendant sur les nom

            var x = db.EDITEUR;
            var a = x.Where(c => c.VILLEED == "paris");
            var b = a.OrderBy(c => c.NOMED); //OrderByDescending
            var ed = b.Select(c => new { c.NOMED, c.VILLEED });

            dataGridView1.DataSource = ed;
        }

        private void button6_Click(object sender, EventArgs e)
        {

            var ed = db.EDITEUR
                .Where(c => c.VILLEED == "paris")
                .OrderByDescending(c => c.NOMED)
                .Select(c => new { c.NOMED, c.VILLEED });


            dataGridView1.DataSource = ed;
        }

        private void button7_Click(object sender, EventArgs e)
        {


            var ed = from edi in db.EDITEUR
                     where (edi.VILLEED == "paris")
                     orderby edi.NOMED //ascending  //descending
                     select new { edi.NOMED, edi.VILLEED };



            dataGridView1.DataSource = ed;
        }

        private void button8_Click(object sender, EventArgs e)
        {



            // liste des editeurs et des ouvrages


            //produit cartesien
            var ed = (from edi in db.EDITEUR
                      from ouv in db.OUVRAGE
                    where edi.NOMED == ouv.NOMED
                      select new { edi.NOMED, ouv.NOMOUVR }).Distinct();

            label1.Text = ed.Count().ToString();

            dataGridView1.DataSource = ed;
        }

        private void button9_Click(object sender, EventArgs e)
        {


            //jointure interne (inner join)
            var ed = (from edi in db.EDITEUR
                      join ouv in db.OUVRAGE
                      on edi.NOMED equals ouv.NOMED
                      select new { edi.NOMED, ouv.NOMOUVR }).Distinct();
            label1.Text = ed.Count().ToString();


            dataGridView1.DataSource = ed;
        }

        private void button10_Click(object sender, EventArgs e)
        {


            // passer à une table sans jointure en utilisant la clé étrangère

            var ed = from ouv in db.OUVRAGE

                     select new { ouv.EDITEUR.VILLEED, ouv.NOMOUVR };


            dataGridView1.DataSource = ed;
        }

        private void button11_Click(object sender, EventArgs e)
        {

            //appliquer distinct sur le resultat de linq

            var ed = (from edi in db.EDITEUR
                      select new { edi.NOMED }).Distinct();
            label1.Text = ed.Count().ToString();


            dataGridView1.DataSource = ed;
        }



    }
}
