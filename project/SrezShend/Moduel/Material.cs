using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;

namespace SrezShend
{
    partial class Material
    {
        public string ValidImage
        {
            get 
            { 
                if (String.IsNullOrWhiteSpace(Image) || String.IsNullOrEmpty(Image)) return @"\img\materials\picture.png";
                else return Image;
            }
        }
        public string ValidSuppliers
        {
            get 
            {
                string suppliers = "";
                List<Supplier> suppliersList = Supplier.ToList();
                if (suppliersList != null && suppliersList.Count() > 0)
                {
                    for (int i = 0; i < suppliersList.Count(); i++)
                    {
                        suppliers += suppliersList[i];
                        if (suppliersList.Last() == suppliersList[i]) suppliers += ".";
                        else suppliers += ", ";
                    }
                }
                else suppliers = "Отсутствуют.";
                return suppliers;
            }
        }

        public Brush MaterialBackground
        {
            get
            {
                DateTime today = DateTime.Now;
                today = today.AddYears(-4);
                foreach (var supplier in Supplier)
                {
                    if (supplier.StartDate > today.AddMonths(-1))
                    {
                        return (Brush)new BrushConverter().ConvertFrom("#ef9a9a");
                    }
                }

                return null;
            }
        }
    }
}
