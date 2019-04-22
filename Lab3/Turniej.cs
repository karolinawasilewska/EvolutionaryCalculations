using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    public class Turniej : Selekcja
    {
        public int RozmiarTurnieju { get; set; }
        public Osobnik Selekcja_Turniej()
        {
            Osobnik[] Osobniki = new Osobnik[RozmiarPopulacji];
            Random rng = new Random();
            Osobnik[] turniej = new Osobnik[RozmiarTurnieju];
            for (int i = 0; i < RozmiarTurnieju; i++)
                turniej[i] = Osobniki[rng.Next(RozmiarPopulacji)];
            return turniej.OrderByDescending(x => Osobnik.FunkcjaDopasowania(x.m_fenotyp)).First(); // zwraca najlepszego z turnieju`
        }
    }
}
