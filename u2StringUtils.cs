using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace cat.cst.u2Array {

  public static class u2StringUtils
  {
    public static String u2Lower(String valor)
    {
      if (u2isEmpty(valor))
      {
        return "";
      }
      // byte[] buf = valor.getBytes();
      char[] buf = valor.ToCharArray();
      int l = buf.Length;
      char b;
      for (int i = 0; i < l; i++)
      {
        b = buf[i];
        switch (b)
        {
          case u2DynArray.RM:
            buf[i] = u2DynArray.AM;
            break;
          case u2DynArray.AM:
            buf[i] = u2DynArray.VM;
            break;
          case u2DynArray.VM:
            buf[i] = u2DynArray.SVM;
            break;
          case u2DynArray.SVM:
            buf[i] = u2DynArray.TM;
            break;
          default:
            break;
        }
      }
      return new String(buf);
    }

    public static String u2Raise(String valor)
    {
      if (u2isEmpty(valor))
      {
        return "";
      }
      // byte[] buf = valor.getBytes();
      char[] buf = valor.ToCharArray();
      int l = buf.Length;
      char b;
      for (int i = 0; i < l; i++)
      {
        b = buf[i];
        switch (b)
        {
          case u2DynArray.AM:
            buf[i] = u2DynArray.RM;
            break;
          case u2DynArray.VM:
            buf[i] = u2DynArray.AM;
            break;
          case u2DynArray.SVM:
            buf[i] = u2DynArray.VM;
            break;
          case u2DynArray.TM:
            buf[i] = u2DynArray.SVM;
            break;
          default:
            break;
        }
      }
      return new String(buf);
    }

    public static int u2Count(String str, char ch)
    {
      if (u2isEmpty(str))
      {
        return 0;
      }

      int count = 0;
      int idx = -1;
      do
      {
        idx = str.IndexOf(ch, ++idx);
        if (idx != -1)
        {
          ++count;
        }
      } while (idx != -1);
      return count;
    }

    public static int u2DCount(String str, char ch)
    {
      if (u2isEmpty(str))
      {
        return 0;
      }
      return u2Count(str, ch) + 1;
    }

    public static Boolean u2isEmpty(String str)
    {
      return ((str == null) || (str.Length == 0));
    }

    public static String u2Field(String cadena, char sep, int pos)
    {
      return (new u2ListStrings(cadena, sep)).Field(pos);
    }

    public static String u2FieldStore(String cadena, char sep, int pos,
            String valor)
    {
      u2ListStrings ls = new u2ListStrings(cadena, sep);
      ls.StoreField(pos, valor);
      return ls.Field(0);
    }


    public static string Utf8ToIso(string utf8String)
    {
      Encoding iso = Encoding.GetEncoding(1252);
      Encoding utf8 = Encoding.UTF8;
      byte[] utfBytes = utf8.GetBytes(utf8String);
      byte[] isoBytes = Encoding.Convert(utf8, iso, utfBytes);
      string isoString = iso.GetString(isoBytes);
      return isoString;
    }

    public static string IsoToUtf8(string isoString)
    {
      Encoding iso = Encoding.GetEncoding(1252);
      Encoding utf8 = Encoding.UTF8;
      byte[] isoBytes = iso.GetBytes(isoString);
      byte[] utfBytes = Encoding.Convert(iso, utf8, isoBytes);
      string utfString = utf8.GetString(utfBytes);
      return utfString;
    }

    public static string u2CString(object str, string def = "")
    {
      string s = def;
      try
      {
        s = Convert.ToString(str);
      }
      catch (Exception ex)
      {
        s = def;
      }
      return s;
    }

    public static int u2Cint(object str, int def = 0)
    {
      int s = def;
      try
      {
        s = Convert.ToInt32(str);
      }
      catch (Exception ex)
      {
        s = def;
      }
      return s;
    }

    public static string u2ToBase64(string valor)
    {
      return Convert.ToBase64String(Encoding.Default.GetBytes(valor));
    }
/*    public static string u2ToBase64(string valor)
    {
      if (valor==null)
      {
        return null;
      }
      else
      {
        return Convert.ToBase64String(Encoding.Default.GetBytes(valor));
      }
    } */

    public static string u2FromBase64(string valorB64)
    {
      if (u2StringUtils.u2isEmpty(valorB64))
      {
        return "";
      }
      else
      {
        return (Encoding.Default.GetString(Convert.FromBase64String(valorB64)));
      }
    }

    public static string u2SQuote(string cadena, bool tractaInteriorCadena = true)
    {
      string res = u2CString(cadena);
      if (tractaInteriorCadena)
      {
        res = res.Replace("'", "''");
      }
      return "'" + res + "'";
    }

    public static string u2StrToHtml(string cadena)
    {
      string s = u2CString(cadena);
      s = s.Replace("&", "&amp;");
      s = s.Replace("<", "&lt;");
      s = s.Replace(">", "&gt;");
      s = s.Replace("\"", "&quot;");
      return s;
    }

    public static string u2Left(string cadena, int numCar)
    {
      string s = "";
      if ((!u2isEmpty(cadena))&&(numCar>0))
      {
        int l = cadena.Length;
        if (l >= numCar) { 
          s = cadena.Substring(0, numCar);
        }
        else
        {
          s = cadena.Substring(0, l);
        }
      }

      return s;
    }

    public static string u2Rigth(string cadena, int numCar)
    {
      string s = "";
      if ((!u2isEmpty(cadena)) && (numCar > 0))
      {
        int l = cadena.Length;
        if (l > numCar)
        {
          s = cadena.Substring(l - numCar, numCar);
        }
        else
        {
          s = cadena;
        }
      }
      return s;
    }

    public static byte[] u2CallGetHttpToByteArray(string url)
    {
      using (HttpClient clienthttpcli = new HttpClient(new HttpClientHandler { Credentials = CredentialCache.DefaultNetworkCredentials }))
      {
        HttpResponseMessage resulthttpcli = clienthttpcli.GetAsync(url).Result;
        if (resulthttpcli.IsSuccessStatusCode)
        {
          return resulthttpcli.Content.ReadAsByteArrayAsync().Result;
        }
        else return null;
      }
    }

    public static byte[] u2CallGetHttpToByteArray(string url, out string contentType)
    {
      contentType = "";
      using (HttpClient clienthttpcli = new HttpClient(new HttpClientHandler { Credentials = CredentialCache.DefaultNetworkCredentials }))
      {
        HttpResponseMessage resulthttpcli = clienthttpcli.GetAsync(url).Result;
        if (resulthttpcli.IsSuccessStatusCode)
        {
          contentType = resulthttpcli.Content.Headers.ContentType.ToString();
          return resulthttpcli.Content.ReadAsByteArrayAsync().Result;
        }
        else return null;
      }
    }

    public static string u2CallGetHttpToString(string url)
    {
      using (HttpClient clienthttpcli = new HttpClient(new HttpClientHandler { Credentials = CredentialCache.DefaultNetworkCredentials }))
      {
        HttpResponseMessage resulthttpcli = clienthttpcli.GetAsync(url).Result;
        if (resulthttpcli.IsSuccessStatusCode)
        {
          return resulthttpcli.Content.ReadAsStringAsync().Result;
        }
        else return null;
      }
    }

    public static string u2CallGetHttpToString(string url, out string contentType)
    {
      contentType = "";
      using (HttpClient clienthttpcli = new HttpClient(new HttpClientHandler { Credentials = CredentialCache.DefaultNetworkCredentials }))
      {
        HttpResponseMessage resulthttpcli = clienthttpcli.GetAsync(url).Result;
        if (resulthttpcli.IsSuccessStatusCode)
        {
          contentType = resulthttpcli.Content.Headers.ContentType.ToString();
          return resulthttpcli.Content.ReadAsStringAsync().Result;
        }
        else return null;
      }
    }
  }
}
