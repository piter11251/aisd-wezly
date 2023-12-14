using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace wezel
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        void A(Wezel w)
        {
            MessageBox.Show(w.ToString());
            foreach (var dziecko in w.dzieci)
                A(dziecko);
        }

        List<Wezel2> odwiedzone = new List<Wezel2>();
        

        void A(Wezel2 w)
        {
            odwiedzone.Add(w);
            MessageBox.Show(w.ToString());
            foreach (var sasiad in w.sasiedzi)
            {
                if (!odwiedzone.Contains(sasiad))
                {                  
                    A(sasiad);
                    
                }        
            }
        }


        void bfs(Wezel2 w)
        {
            
            Queue<Wezel2> doOdwiedzenia = new Queue<Wezel2>();

            doOdwiedzenia.Enqueue(w);
            odwiedzone.Add(w);

            while (doOdwiedzenia.Count > 0)
            {
                Wezel2 aktualnyWezel = doOdwiedzenia.Dequeue();
                MessageBox.Show(aktualnyWezel.ToString());

                foreach (var sasiad in aktualnyWezel.sasiedzi)
                {
                    if (!odwiedzone.Contains(sasiad))
                    {
                        doOdwiedzenia.Enqueue(sasiad);
                        odwiedzone.Add(sasiad);
                    }
                }
            }
        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void btn1_Click(object sender, EventArgs e)
        {
            var wezel1 = new Wezel(5);
            var wezel2 = new Wezel(2);
            var wezel3 = new Wezel(1);
            var wezel4 = new Wezel(3);
            var wezel5 = new Wezel(4);
            var wezel6 = new Wezel(6);

            wezel1.dzieci.Add(wezel2);
            wezel1.dzieci.Add(wezel3);
            wezel1.dzieci.Add(wezel4);
            wezel2.dzieci.Add(wezel5);
            wezel2.dzieci.Add(wezel6);

            A(wezel1);

            var listaWezlow = new List<Wezel>();
            listaWezlow.Add(wezel1);
            listaWezlow.Add(wezel2);
            listaWezlow.Add(wezel3);
            listaWezlow.Add(wezel4);
            listaWezlow.Add(wezel5);
            listaWezlow.Add(wezel6);

            cbWezly.Items.AddRange(listaWezlow.ToArray());

        }

        private void cbWezly_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btn2_Click(object sender, EventArgs e)
        {
            var w1 = new Wezel2(7);
            var w2 = new Wezel2(2);
            var w3 = new Wezel2(5);
            var w4 = new Wezel2(9);
            var w5 = new Wezel2(4);
            var w6 = new Wezel2(1);
            var w7 = new Wezel2(3);

            w1.Add(w2);
            w1.Add(w3);

            w2.Add(w1);
            w2.Add(w4);

            w3.Add(w5);
            w3.Add(w6);

            w4.Add(w7);

            w5.Add(w7);

            w7.Add(w5);

            A(w1);
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            var w1 = new Wezel2(7);
            var w2 = new Wezel2(2);
            var w3 = new Wezel2(5);
            var w4 = new Wezel2(9);
            var w5 = new Wezel2(4);
            var w6 = new Wezel2(1);
            var w7 = new Wezel2(3);

            w1.Add(w2);
            w1.Add(w3);

            w2.Add(w1);
            w2.Add(w4);

            w3.Add(w5);
            w3.Add(w6);

            w4.Add(w7);

            w5.Add(w7);

            w7.Add(w5);

            bfs(w1);
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            var d = new DrzewoBinarne(5);

            d.Add(3);
            d.Add(8);
            d.Add(4);
            d.Add(7);
            d.Add(6);
            d.Add(5);
            d.Add(6);


            MessageBox.Show(d.ZnajdzMin(d.korzen).wartosc.ToString());
            MessageBox.Show(d.ZnajdzMax(d.korzen).wartosc.ToString());
            //var costam = d.korzen.praweDziecko;
            //MessageBox.Show(d.Nastepnik(costam).wartosc.ToString());
            var costam = d.korzen.praweDziecko;
            MessageBox.Show(d.Poprzednik(costam).wartosc.ToString());

        }
    }
    public class Wezel
    {
        public int wartosc;
        public List<Wezel> dzieci = new List<Wezel>();

        public Wezel(int liczba)
        {
            this.wartosc = liczba;
        }

        public override string ToString()
        {
            return "Wartość: " + this.wartosc.ToString();
        }
    }

    public class Wezel2
    {
        public int wartosc;
        public List<Wezel2> sasiedzi = new List<Wezel2>();

        public Wezel2(int liczba)
        {
            this.wartosc = liczba;
        }

        public void Add(Wezel2 w)
        {
            this.sasiedzi.Add(w);
            w.sasiedzi.Add(this);
        }

        public override string ToString()
        {
            return "Wartość: " + this.wartosc.ToString();
        }
    }

    public class Wezel3
    {
        public int wartosc;
        public Wezel3 rodzic;
        public Wezel3 leweDziecko;
        public Wezel3 praweDziecko;

        public Wezel3(int liczba)
        {
            this.wartosc = liczba;
        }

        public override string ToString()
        {
            return "Wartość: " + this.wartosc.ToString();
        }

        public void Add(int liczba)
        {
            var dziecko = new Wezel3(liczba);
            dziecko.rodzic = this;
            if (liczba < this.wartosc)
                this.leweDziecko = dziecko;
            else
                this.praweDziecko = dziecko;
        }
    }

    class DrzewoBinarne
    {
        public Wezel3 korzen;
        public int liczbaWezlow;
        public DrzewoBinarne(int liczba)
        {
            this.korzen = new Wezel3(liczba);
            this.liczbaWezlow = 1;
        }

        public void Add(int liczba)
        {
            liczbaWezlow++;
            var rodzic = this.znajdzWezel(liczba);
            rodzic.Add(liczba);

        }

        public Wezel3 znajdzWezel(int liczba)
        {
            var w = this.korzen;
            while (true)
            {
                if (liczba < w.wartosc)
                {
                    if (w.leweDziecko != null)
                        w = w.leweDziecko;
                    else
                        return w;
                }
                else
                {
                    if (w.praweDziecko != null)
                        w = w.praweDziecko;
                    else
                        return w;
                }
            }
        }

        public Wezel3 Znajdz(int liczba)
        {
            var w = this.korzen;
            while (true)
            {
                if(liczba < w.wartosc)
                {
                    if (w.wartosc == liczba)
                        return w;
                    else
                        w = w.leweDziecko;
                }
                else
                {
                    if (w.wartosc == liczba)
                        return w;
                    else
                        w = w.praweDziecko;
                }
            }
        }

        public Wezel3 ZnajdzMin(Wezel3 w)
        {
            var min = w;
            while(w.leweDziecko != null)
            {
                if(w.leweDziecko.wartosc < min.wartosc)
                    min = w.leweDziecko;
                w = w.leweDziecko;
            }
            return min;
        }

        public Wezel3 ZnajdzMax(Wezel3 w)
        {
            var max = w;
            while (w.praweDziecko != null)
            {
                if (w.praweDziecko.wartosc > max.wartosc)
                    max = w.praweDziecko;
                w = w.praweDziecko;
            }
            return max;
        }

        public Wezel3 Nastepnik(Wezel3 w)
        {
            if (w.praweDziecko != null)
            {
                var min = ZnajdzMin(w.praweDziecko);
                return min;
            }
            else if (w.praweDziecko == null)
            {
                while (w.rodzic != null)
                {
                    if (w.rodzic.leweDziecko == w)
                        return w.rodzic;
                    else
                        w = w.rodzic;
                }
            }
            else if(w.rodzic == null)
                return null;
            return null;
        }

        public Wezel3 Poprzednik(Wezel3 w)
        {
            if (w.leweDziecko != null)
            {
                var max = ZnajdzMax(w.leweDziecko);
                return max;
            }
            else if(w.leweDziecko == null)
            {
                while(w.rodzic !=null)
                {
                    if (w.rodzic.praweDziecko == w)
                        return w.rodzic;
                    else
                        w = w.rodzic;
                }
            }
            else if (w.rodzic == null)
                return null;
            return null;
        }

        public Wezel3 Usun(Wezel3 w)
        {
            if (w.leweDziecko == null && w.praweDziecko == null)
            {
                if (w == w.rodzic.praweDziecko)
                {
                    w.rodzic.praweDziecko = null;
                }
                else
                {
                    w.rodzic.leweDziecko = null;
                }
                w.rodzic = null;
                return w;
            }
            else if (w.leweDziecko == null && w.praweDziecko != null)
            {
                if (w == w.rodzic.praweDziecko)
                {
                    w.praweDziecko.rodzic = w.rodzic;
                    w.rodzic.praweDziecko = w.praweDziecko;

                }
                else
                {
                    w.praweDziecko.rodzic = w.rodzic;
                    w.rodzic.leweDziecko = w.praweDziecko;
                }
                w.rodzic = null;
                w.praweDziecko = null;
                return w;

            }
            else if (w.leweDziecko != null && w.praweDziecko == null)
            {
                if (w == w.rodzic.praweDziecko)
                {
                    w.leweDziecko.rodzic = w.rodzic;
                    w.rodzic.praweDziecko = w.leweDziecko;

                }
                else
                {
                    w.leweDziecko.rodzic = w.rodzic;
                    w.rodzic.leweDziecko = w.leweDziecko;
                }
                w.rodzic = null;
                w.leweDziecko = null;
                return w;
            }
            else
            {
                var nastepnik = this.Nastepnik(w);
                if (nastepnik.leweDziecko == null && nastepnik.praweDziecko == null)
                {
                    if (w == w.rodzic.leweDziecko)
                    {
                        w.rodzic.leweDziecko = nastepnik;
                    }
                    else
                    {
                        w.rodzic.praweDziecko = nastepnik;
                    }
                    nastepnik.rodzic = w.rodzic;
                    nastepnik.leweDziecko = w.leweDziecko;
                    nastepnik.praweDziecko = w.praweDziecko;
                }
                else if (nastepnik.leweDziecko == null && nastepnik.praweDziecko != null)
                {
                    nastepnik.praweDziecko.rodzic = nastepnik.rodzic;
                    nastepnik.rodzic.leweDziecko = nastepnik.praweDziecko;

                    nastepnik.rodzic = w.rodzic;
                    if (w == w.rodzic.leweDziecko)
                    {
                        w.rodzic.leweDziecko = nastepnik;
                    }
                    else
                    {
                        w.rodzic.praweDziecko = nastepnik;
                    }
                    nastepnik.rodzic = w.rodzic;
                    nastepnik.leweDziecko = w.leweDziecko;
                    nastepnik.praweDziecko = w.praweDziecko;
                }
                w.rodzic = null;
                w.leweDziecko = null;
                w.praweDziecko = null;
                return w;
            }
        }
    }

}
