using System;
using System.Collections.Generic;
using System.Text;

namespace Lab8.Selecions
{
    public interface ISelection
    {
        Individual Select(Individual[] individuals, int size);
    }
}
