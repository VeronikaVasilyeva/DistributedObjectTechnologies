//
// Autogenerated by Thrift Compiler (0.9.3)
//
// DO NOT EDIT UNLESS YOU ARE SURE THAT YOU KNOW WHAT YOU ARE DOING
//


if (typeof OpenGraph === 'undefined') {
  OpenGraph = {};
}
OpenGraph.OpenGraphMeta = function(args) {
  this.title = null;
  this.type = null;
  this.image = null;
  this.url = null;
  if (args) {
    if (args.title !== undefined && args.title !== null) {
      this.title = args.title;
    }
    if (args.type !== undefined && args.type !== null) {
      this.type = args.type;
    }
    if (args.image !== undefined && args.image !== null) {
      this.image = args.image;
    }
    if (args.url !== undefined && args.url !== null) {
      this.url = args.url;
    }
  }
};
OpenGraph.OpenGraphMeta.prototype = {};
OpenGraph.OpenGraphMeta.prototype.read = function(input) {
  input.readStructBegin();
  while (true)
  {
    var ret = input.readFieldBegin();
    var fname = ret.fname;
    var ftype = ret.ftype;
    var fid = ret.fid;
    if (ftype == Thrift.Type.STOP) {
      break;
    }
    switch (fid)
    {
      case 1:
      if (ftype == Thrift.Type.STRING) {
        this.title = input.readString().value;
      } else {
        input.skip(ftype);
      }
      break;
      case 2:
      if (ftype == Thrift.Type.STRING) {
        this.type = input.readString().value;
      } else {
        input.skip(ftype);
      }
      break;
      case 3:
      if (ftype == Thrift.Type.STRING) {
        this.image = input.readString().value;
      } else {
        input.skip(ftype);
      }
      break;
      case 4:
      if (ftype == Thrift.Type.STRING) {
        this.url = input.readString().value;
      } else {
        input.skip(ftype);
      }
      break;
      default:
        input.skip(ftype);
    }
    input.readFieldEnd();
  }
  input.readStructEnd();
  return;
};

OpenGraph.OpenGraphMeta.prototype.write = function(output) {
  output.writeStructBegin('OpenGraphMeta');
  if (this.title !== null && this.title !== undefined) {
    output.writeFieldBegin('title', Thrift.Type.STRING, 1);
    output.writeString(this.title);
    output.writeFieldEnd();
  }
  if (this.type !== null && this.type !== undefined) {
    output.writeFieldBegin('type', Thrift.Type.STRING, 2);
    output.writeString(this.type);
    output.writeFieldEnd();
  }
  if (this.image !== null && this.image !== undefined) {
    output.writeFieldBegin('image', Thrift.Type.STRING, 3);
    output.writeString(this.image);
    output.writeFieldEnd();
  }
  if (this.url !== null && this.url !== undefined) {
    output.writeFieldBegin('url', Thrift.Type.STRING, 4);
    output.writeString(this.url);
    output.writeFieldEnd();
  }
  output.writeFieldStop();
  output.writeStructEnd();
  return;
};

OpenGraph.NetException = function(args) {
};
Thrift.inherits(OpenGraph.NetException, Thrift.TException);
OpenGraph.NetException.prototype.name = 'NetException';
OpenGraph.NetException.prototype.read = function(input) {
  input.readStructBegin();
  while (true)
  {
    var ret = input.readFieldBegin();
    var fname = ret.fname;
    var ftype = ret.ftype;
    var fid = ret.fid;
    if (ftype == Thrift.Type.STOP) {
      break;
    }
    input.skip(ftype);
    input.readFieldEnd();
  }
  input.readStructEnd();
  return;
};

OpenGraph.NetException.prototype.write = function(output) {
  output.writeStructBegin('NetException');
  output.writeFieldStop();
  output.writeStructEnd();
  return;
};

OpenGraph.NotFoundException = function(args) {
};
Thrift.inherits(OpenGraph.NotFoundException, Thrift.TException);
OpenGraph.NotFoundException.prototype.name = 'NotFoundException';
OpenGraph.NotFoundException.prototype.read = function(input) {
  input.readStructBegin();
  while (true)
  {
    var ret = input.readFieldBegin();
    var fname = ret.fname;
    var ftype = ret.ftype;
    var fid = ret.fid;
    if (ftype == Thrift.Type.STOP) {
      break;
    }
    input.skip(ftype);
    input.readFieldEnd();
  }
  input.readStructEnd();
  return;
};

OpenGraph.NotFoundException.prototype.write = function(output) {
  output.writeStructBegin('NotFoundException');
  output.writeFieldStop();
  output.writeStructEnd();
  return;
};

OpenGraph.UnknownException = function(args) {
};
Thrift.inherits(OpenGraph.UnknownException, Thrift.TException);
OpenGraph.UnknownException.prototype.name = 'UnknownException';
OpenGraph.UnknownException.prototype.read = function(input) {
  input.readStructBegin();
  while (true)
  {
    var ret = input.readFieldBegin();
    var fname = ret.fname;
    var ftype = ret.ftype;
    var fid = ret.fid;
    if (ftype == Thrift.Type.STOP) {
      break;
    }
    input.skip(ftype);
    input.readFieldEnd();
  }
  input.readStructEnd();
  return;
};

OpenGraph.UnknownException.prototype.write = function(output) {
  output.writeStructBegin('UnknownException');
  output.writeFieldStop();
  output.writeStructEnd();
  return;
};

OpenGraph.MetaNotFoundException = function(args) {
};
Thrift.inherits(OpenGraph.MetaNotFoundException, Thrift.TException);
OpenGraph.MetaNotFoundException.prototype.name = 'MetaNotFoundException';
OpenGraph.MetaNotFoundException.prototype.read = function(input) {
  input.readStructBegin();
  while (true)
  {
    var ret = input.readFieldBegin();
    var fname = ret.fname;
    var ftype = ret.ftype;
    var fid = ret.fid;
    if (ftype == Thrift.Type.STOP) {
      break;
    }
    input.skip(ftype);
    input.readFieldEnd();
  }
  input.readStructEnd();
  return;
};

OpenGraph.MetaNotFoundException.prototype.write = function(output) {
  output.writeStructBegin('MetaNotFoundException');
  output.writeFieldStop();
  output.writeStructEnd();
  return;
};

