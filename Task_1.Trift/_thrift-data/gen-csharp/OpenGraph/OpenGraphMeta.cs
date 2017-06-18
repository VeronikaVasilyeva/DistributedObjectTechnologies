/**
 * Autogenerated by Thrift Compiler (0.9.3)
 *
 * DO NOT EDIT UNLESS YOU ARE SURE THAT YOU KNOW WHAT YOU ARE DOING
 *  @generated
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Thrift;
using Thrift.Collections;
using System.Runtime.Serialization;
using Thrift.Protocol;
using Thrift.Transport;

namespace OpenGraph
{

  #if !SILVERLIGHT
  [Serializable]
  #endif
  public partial class OpenGraphMeta : TBase
  {
    private string _title;
    private string _type;
    private string _image;
    private string _url;

    public string Title
    {
      get
      {
        return _title;
      }
      set
      {
        __isset.title = true;
        this._title = value;
      }
    }

    public string Type
    {
      get
      {
        return _type;
      }
      set
      {
        __isset.type = true;
        this._type = value;
      }
    }

    public string Image
    {
      get
      {
        return _image;
      }
      set
      {
        __isset.image = true;
        this._image = value;
      }
    }

    public string Url
    {
      get
      {
        return _url;
      }
      set
      {
        __isset.url = true;
        this._url = value;
      }
    }


    public Isset __isset;
    #if !SILVERLIGHT
    [Serializable]
    #endif
    public struct Isset {
      public bool title;
      public bool type;
      public bool image;
      public bool url;
    }

    public OpenGraphMeta() {
    }

    public void Read (TProtocol iprot)
    {
      iprot.IncrementRecursionDepth();
      try
      {
        TField field;
        iprot.ReadStructBegin();
        while (true)
        {
          field = iprot.ReadFieldBegin();
          if (field.Type == TType.Stop) { 
            break;
          }
          switch (field.ID)
          {
            case 1:
              if (field.Type == TType.String) {
                Title = iprot.ReadString();
              } else { 
                TProtocolUtil.Skip(iprot, field.Type);
              }
              break;
            case 2:
              if (field.Type == TType.String) {
                Type = iprot.ReadString();
              } else { 
                TProtocolUtil.Skip(iprot, field.Type);
              }
              break;
            case 3:
              if (field.Type == TType.String) {
                Image = iprot.ReadString();
              } else { 
                TProtocolUtil.Skip(iprot, field.Type);
              }
              break;
            case 4:
              if (field.Type == TType.String) {
                Url = iprot.ReadString();
              } else { 
                TProtocolUtil.Skip(iprot, field.Type);
              }
              break;
            default: 
              TProtocolUtil.Skip(iprot, field.Type);
              break;
          }
          iprot.ReadFieldEnd();
        }
        iprot.ReadStructEnd();
      }
      finally
      {
        iprot.DecrementRecursionDepth();
      }
    }

    public void Write(TProtocol oprot) {
      oprot.IncrementRecursionDepth();
      try
      {
        TStruct struc = new TStruct("OpenGraphMeta");
        oprot.WriteStructBegin(struc);
        TField field = new TField();
        if (Title != null && __isset.title) {
          field.Name = "title";
          field.Type = TType.String;
          field.ID = 1;
          oprot.WriteFieldBegin(field);
          oprot.WriteString(Title);
          oprot.WriteFieldEnd();
        }
        if (Type != null && __isset.type) {
          field.Name = "type";
          field.Type = TType.String;
          field.ID = 2;
          oprot.WriteFieldBegin(field);
          oprot.WriteString(Type);
          oprot.WriteFieldEnd();
        }
        if (Image != null && __isset.image) {
          field.Name = "image";
          field.Type = TType.String;
          field.ID = 3;
          oprot.WriteFieldBegin(field);
          oprot.WriteString(Image);
          oprot.WriteFieldEnd();
        }
        if (Url != null && __isset.url) {
          field.Name = "url";
          field.Type = TType.String;
          field.ID = 4;
          oprot.WriteFieldBegin(field);
          oprot.WriteString(Url);
          oprot.WriteFieldEnd();
        }
        oprot.WriteFieldStop();
        oprot.WriteStructEnd();
      }
      finally
      {
        oprot.DecrementRecursionDepth();
      }
    }

    public override string ToString() {
      StringBuilder __sb = new StringBuilder("OpenGraphMeta(");
      bool __first = true;
      if (Title != null && __isset.title) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("Title: ");
        __sb.Append(Title);
      }
      if (Type != null && __isset.type) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("Type: ");
        __sb.Append(Type);
      }
      if (Image != null && __isset.image) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("Image: ");
        __sb.Append(Image);
      }
      if (Url != null && __isset.url) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("Url: ");
        __sb.Append(Url);
      }
      __sb.Append(")");
      return __sb.ToString();
    }

  }

}