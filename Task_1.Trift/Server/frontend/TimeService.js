//HELPER FUNCTIONS AND STRUCTURES

TimeServer.Thrift.TimeService_GetTime_args = function(args){
}
TimeServer.Thrift.TimeService_GetTime_args.prototype = {}
TimeServer.Thrift.TimeService_GetTime_args.prototype.read = function(input){ 
var ret = input.readStructBegin()
while (1) 
{
var ret = input.readFieldBegin()
var fname = ret.fname
var ftype = ret.ftype
var fid   = ret.fid
if (ftype == Thrift.Type.STOP) 
  break
switch(fid)
{
  default:
    input.skip(ftype)
}
input.readFieldEnd()
}
input.readStructEnd()
return
}

TimeServer.Thrift.TimeService_GetTime_args.prototype.write = function(output){ 
output.writeStructBegin('TimeService_GetTime_args')
output.writeFieldStop()
output.writeStructEnd()
return
}

TimeServer.Thrift.TimeService_GetTime_result = function(args){
this.success = new TimeServer.Thrift.TimeInfoStruct()
if( args != null ){if (null != args.success)
this.success = args.success
}}
TimeServer.Thrift.TimeService_GetTime_result.prototype = {}
TimeServer.Thrift.TimeService_GetTime_result.prototype.read = function(input){ 
var ret = input.readStructBegin()
while (1) 
{
var ret = input.readFieldBegin()
var fname = ret.fname
var ftype = ret.ftype
var fid   = ret.fid
if (ftype == Thrift.Type.STOP) 
break
switch(fid)
{
case 0:if (ftype == Thrift.Type.STRUCT) {
  this.success = new TimeServer.Thrift.TimeInfoStruct()
  this.success.read(input)
} else {
  input.skip(ftype)
}
break
default:
  input.skip(ftype)
}
input.readFieldEnd()
}
input.readStructEnd()
return
}

TimeServer.Thrift.TimeService_GetTime_result.prototype.write = function(output){ 
output.writeStructBegin('TimeService_GetTime_result')
if (null != this.success) {
output.writeFieldBegin('success', Thrift.Type.STRUCT, 0)
this.success.write(output)
output.writeFieldEnd()
}
output.writeFieldStop()
output.writeStructEnd()
return
}

TimeServer.Thrift.TimeServiceClient = function(input, output) {
  this.input  = input
  this.output = null == output ? input : output
  this.seqid  = 0
}
TimeServer.Thrift.TimeServiceClient.prototype = {}
TimeServer.Thrift.TimeServiceClient.prototype.GetTime = function(){
this.send_GetTime()
return this.recv_GetTime()
}

TimeServer.Thrift.TimeServiceClient.prototype.send_GetTime = function(){
this.output.writeMessageBegin('GetTime', Thrift.MessageType.CALL, this.seqid)
var args = new TimeServer.Thrift.TimeService_GetTime_args()
args.write(this.output)
this.output.writeMessageEnd()
return this.output.getTransport().flush()
}

TimeServer.Thrift.TimeServiceClient.prototype.recv_GetTime = function(){
var ret = this.input.readMessageBegin()
var fname = ret.fname
var mtype = ret.mtype
var rseqid= ret.rseqid
if (mtype == Thrift.MessageType.EXCEPTION) {
  var x = new Thrift.ApplicationException()
  x.read(this.input)
  this.input.readMessageEnd()
  throw x
}
var result = new TimeServer.Thrift.TimeService_GetTime_result()
result.read(this.input)
this.input.readMessageEnd()

if (null != result.success ) {
  return result.success
}
throw "GetTime failed: unknown result"
}
