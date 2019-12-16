using System;
using System.Collections.Generic;
using System.Text;

namespace cat.cst.u2Array
{
    public class u2DynArray
    {
	  public const char RM = (char)255;
      public const char AM = (char)254;
      public const char VM = (char)253;
      public const char SVM = (char)252;
      public const char TM = (char)251;

	    private u2ListStrings dataAM = new u2ListStrings(AM);
	    private u2ListStrings dataVM;
	    private u2ListStrings dataSVM;
	    private int lastX = -2;
	    private int lastY = -2;

        // private ListStrings dataTM;

        private void getDataVM(int X)
        {
            if (lastX != X)
            {
                dataVM = new u2ListStrings(dataAM.Field(X), VM);
            }
        }

    public void replace(int v, object p)
    {
      throw new NotImplementedException();
    }

    private void getDataSVM(int X, int Y)
    {
        Boolean b = false;
        if (lastX != X)
        {
            dataVM = new u2ListStrings(dataAM.Field(X), VM);
            b = true;
        }
        if ((b) || (lastY != Y))
        {
            dataSVM = new u2ListStrings(dataVM.Field(Y), SVM);
        }
    }

    private void setLastX(int X, String valor)
    {
        if (valor.IndexOf(AM) < 0)
        {
            lastX = X;
        }
        else
        {
            lastX = -2;
        }
        lastY = -2;
    }

    private void setLastXY(int X, int Y, String valor)
    {
        Boolean b = false;
        b = valor.IndexOf(AM) < 0;
        if (b)
        {
            lastX = X;
            if ((b) && (valor.IndexOf(VM) < 0))
            {
                lastY = Y;
            }
            else
            {
                lastY = -2;
            }
        }
        else
        {
            lastX = -2;
            lastY = -2;
        }

    }

    public u2DynArray()
    {
    }

    public u2DynArray(String valor)
    {
        // dataAM = new u2ListStrings(AM);
        replace(valor);
    }

    public String extract()
    {
        return dataAM.Field(0);
    }

    public String extract(int X)
    {
        return dataAM.Field(X);
    }

    public String extract(int X, int Y)
    {
        getDataVM(X);
        if (lastX != X)
        {
            lastX = X; lastY = -2;
        }
        return dataVM.Field(Y);
    }

    public String extract(int X, int Y, int Z)
    {
        getDataSVM(X, Y);
        lastX = X;
        lastY = Y;
        return dataSVM.Field(Z);
    }

    public void replace(String valor)
    {
        dataAM.StoreField(0, valor);
        lastX = -2;
        lastY = -2;
    }

    public void replace(int X, String valor)
    {
        dataAM.StoreField(X, valor);
        lastX = -2;
        lastY = -2;
    }

    public void replace(int X, int Y, String valor)
    {
        getDataVM(X);
        dataVM.StoreField(Y, valor);
        dataAM.StoreField(X, dataVM.Field(0));
        setLastX(X, valor);
    }

    public void replace(int X, int Y, int Z, String valor)
    {
        getDataSVM(X, Y);
        dataSVM.StoreField(Z, valor);
        dataVM.StoreField(Y, dataSVM.Field(0));
        dataAM.StoreField(X, dataVM.Field(0));
        setLastXY(X, Y, valor);
    }

    public void insert(int X, String valor)
    {
        dataAM.InsertField(X, valor);
        lastX = -2;
        lastY = -2;
    }

    public void insert(int X, int Y, String valor)
    {
        getDataVM(X);
        dataVM.InsertField(Y, valor);
        dataAM.StoreField(X, dataVM.Field(0));
        setLastX(X, valor);
    }

    public void insert(int X, int Y, int Z, String valor)
    {
        getDataSVM(X, Y);
        dataSVM.InsertField(Z, valor);
        dataVM.StoreField(Y, dataSVM.Field(0));
        dataAM.StoreField(X, dataVM.Field(0));
        setLastXY(X, Y, valor);
    }

    public void delete(int X)
    {
        dataAM.DeleteField(X);
        lastX = -2; lastY = -2;
    }

    public void delete(int X, int Y)
    {
        getDataVM(X);
        dataVM.DeleteField(Y);
        dataAM.StoreField(X, dataVM.Field(0));
        lastX = X;
        lastY = -2;
    }

    public void delete(int X, int Y, int Z)
    {
        getDataSVM(X, Y);
        dataSVM.DeleteField(Z);
        dataVM.StoreField(Y, dataSVM.Field(0));
        dataAM.StoreField(X, dataVM.Field(0));
        lastX = X;
        lastY = Y;
    }

    public int locate(String cadena)
    {
        return dataAM.Locate(cadena);
    }

    public int locate(String cadena, int X)
    {
        getDataVM(X);
        lastX = X;
        lastY = -2;
        return dataVM.Locate(cadena);
    }

    public int locate(String cadena, int X, int Y)
    {
        getDataSVM(X, Y);
        lastX = X;
        lastY = Y;
        return dataSVM.Locate(cadena);
    }

    public int DCount()
    {
      return dataAM.DCount();
    }

    public int DCount(int X)
    {
      getDataVM(X);
      if (lastX != X)
      {
        lastX = X; lastY = -2;
      }
      return dataVM.DCount();
    }

    public int DCount(int X, int Y)
    {
      getDataSVM(X, Y);
      lastX = X;
      lastY = Y;
      return dataSVM.DCount();
    }
    public String getBase64()
    {
      return Convert.ToBase64String(Encoding.Default.GetBytes(extract()));
    }

    public void setBase64(String valorB64)
    {
      if (u2StringUtils.u2isEmpty(valorB64))
      {
        replace("");
      }
      else
      {
        replace(Encoding.Default.GetString(Convert.FromBase64String(valorB64)));
      }
    }
    public string AsString
    {
      get { return this.extract(); }
      set { this.replace(value); }
    }

    public string this[int fieldNo]
    {
      get { return this.extract(fieldNo); }
      set { replace(fieldNo, value); }
    }
    public string this[int fieldNo, int valueNo]
    {
      get { return this.extract(fieldNo, valueNo); }
      set { replace(fieldNo, valueNo, value); }
    }
    public string this[int fieldNo, int valueNo, int subValueNo]
    {
      get { return this.extract(fieldNo, valueNo, subValueNo); }
      set { replace(fieldNo, valueNo, subValueNo, value); }
    }
  }
}
