using System;
using System.Collections.Generic;
using System.Text;

namespace cat.cst.u2Array
{

  public class u2ListStrings
  {
    private List<string> dades;
            
    char sep;

    public string[] ToArray()
    {
      return dades.ToArray();
    }

    public List<string> ToList()
    {
      return dades;
    }

    public u2ListStrings(List<string> llista, char charsep)
    {
      dades = llista;
      sep = charsep;
    }

    public u2ListStrings(String cadena, char charsep)
    {
      dades = new List<string>();
      sep = charsep;
      u2Split(cadena);
    }

    public u2ListStrings(char charsep)
    {
      sep = charsep;
      dades = new List<string>();
    }

    public void u2Split(String str) {
      dades.Clear();
      dades.AddRange(str.Split(sep));
    }

    public string u2Join() {
      if((null == dades) || (dades.Count==0 )) {
        return "" ;
      }
      return string.Join(sep.ToString(), dades.ToArray());
    }

    public string u2Join(int inici, int numcamps)
    {
      if ((null == dades) || (dades.Count == 0))
      {
        return "";
      }
      return string.Join(sep.ToString(), dades.ToArray(), inici-1, numcamps);
    }

    public string Field(int pos)
    {
      if (( pos<0 ) || ( pos>dades.Count)) {
        return "" ;
      }
      if (pos==0) {
        return u2Join();
      }
      return dades[ pos-1 ];
    }

    public string Field(int pos, int cnt)
    {
      if ((pos < 0) || (pos > dades.Count))
      {
        return "";
      }
      if (pos == 0)
      {
        return u2Join();
      }
      return u2Join(pos,cnt);
    }

    public void StoreField(int pos, String valor) 
    {
      Boolean bCal = false;
      if (pos < 0)
      {
        dades.Add(valor);
        bCal = true;
      }
      else if (pos == 0)
      {
        u2Split(valor);
      }
      else if (pos <= dades.Count)
      {
        dades[pos - 1] = valor;
        bCal = true;
      }
      else
      {
        for (int i = dades.Count; i < pos; i++)
        {
          dades.Add("");
        }
        dades[pos - 1] = valor;
        bCal = true;
      }
      if ((bCal) && (valor.IndexOf(sep) != -1))
      {
        u2Split(u2Join());
      }
    }

    public void InsertField(int pos, String valor) 
    {
      Boolean bCal = false;
      if (pos < 0)
      {
        dades.Add(valor);
        bCal = true;
      }
      else if (pos == 0)
      {
        dades.Insert(0, valor);
        bCal = true;
      }
      else if (pos <= dades.Count)
      {
        dades.Insert(pos - 1, valor);
        bCal = true;
      }
      else
      {
        StoreField(pos, valor);
      }
      if ((bCal) && (valor.IndexOf(sep) != -1))
      {
        u2Split(u2Join());
      }
    }

    public void DeleteField(int pos) 
    {
		  if (pos < 0) {
			  dades.RemoveAt(dades.Count -1);
		  } else if ((pos > 0)&&(pos <= dades.Count)) {
        dades.RemoveAt(pos - 1);
		  }
	  }

    public int DCount()
    {
      if (dades.Count == 0)
      {
        return 0;
      }
      else if ((dades.Count == 1) && (isEmpty(dades[0])))
      {
        return 0;
      }
      else
      {
        return dades.Count;
      }
    }

    public int Locate(String cadena)
    {
      return dades.IndexOf(cadena) + 1;
    }

    private static Boolean isEmpty(String str)
    {
      return ((null == str) || (str.Length == 0));
    }
  }

}

