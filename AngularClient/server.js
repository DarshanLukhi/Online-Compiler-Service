var express = require('express');
var bodyParser = require('body-parser');
var cors = require('cors');
var soap = require('soap');
var app = express();

app.use(bodyParser.urlencoded({extended:false}));
app.use(bodyParser.json());
app.use(bodyParser.raw());
app.use(cors());
const port = 3000;

const server = app.listen(port, function(){
 console.log('Listening on port ' + port);
});



app.use(function(req,res,next){
  res.header('Access-Control-Allow-Origin','*');
  res.header('Access-Control-Allow-Methods','GET,PUT,POST,DELETE,OPTIONS');
  res.header('ACCESS_Control-Allow-Headers','*');

  if('OPTIONS' === req.method)
  {
    return res.sendStatus(200);
  }
  next();
});

app.post('/compilecode' , function (req , res ) {
  var url = 'http://localhost:8888/CompilerService?wsdl';
  var lang = req.body.lang;

  if(req.body.input === ""){
    input = false;
    var args = {
      code : req.body.code,
      key : 'PFTYMbM7l4YkcJGtH9tcKfBrx0fyEaGD'
    };
  }
  else{
    input = true;
    var args = {
      code : req.body.code,
      input : req.body.input,
      key : 'PFTYMbM7l4YkcJGtH9tcKfBrx0fyEaGD'
    };
  }
  

  
  soap.createClient(url, function(err, client) {
    if(lang == "C" && input)
    client.CompileCWithInput(args, function(err, result) {
      res.send(result.CompileCWithInputResult);
    });
    else if(lang == "C++" && input)
    client.CompileCPPWithInput(args, function(err, result) {
      res.send(result.CompileCPPWithInputResult);
    });
    else if(lang == "Java" && input)
    client.CompileJavaWithInput(args, function(err, result) {
      res.send(result.CompileJavaWithInputResult);
    });
    else if(lang == "Python" && input)
    client.CompilePythonWithInput(args, function(err, result) {
      res.send(result.CompilePythonWithInputResult);
    });
    else if(lang == "C" && !input)
    client.CompileC(args, function(err, result) {
      console.log(result.CompileCResult);
      res.send(result.CompileCResult);
    });
    else if(lang == "C++" && !input)
    client.CompileCPP(args, function(err, result) {
      console.log(result.CompileCPPResult);
      res.send(result.CompileCPPResult);
    });
    else if(lang == "Java" && !input)
    client.CompileJava(args, function(err, result) {
      console.log(result.CompileJavaResult);
      res.send(result.CompileJavaResult);
    });
    else if(lang == "Python" && !input)
    client.CompilePython(args, function(err, result) {
      console.log(result.CompilePythonResult);
      res.send(result.CompilePythonResult);
    });
  });

  
});

app.get('/getHistory' , function (req , res ) {
  var url = 'http://localhost:8888/CompilerService?wsdl';
  var args = {
    key : 'PFTYMbM7l4YkcJGtH9tcKfBrx0fyEaGD'
  };
  soap.createClient(url, function(err, client) {
    client.ShowAllCode(args, function(err, result) {
      res.send(result.ShowAllCodeResult);
    });
  });

});

app.post('/getFile' , function (req , res ) {
  var url = 'http://localhost:8888/CompilerService?wsdl';
  var CodeID = req.body.CodeID;
  var args = {
    CodeID : CodeID,
    key : 'PFTYMbM7l4YkcJGtH9tcKfBrx0fyEaGD'
  };
  soap.createClient(url, function(err, client) {
    client.GetCode(args, function(err, result) {
 
;      res.send(result.GetCodeResult);
    });
  });

});

app.post('/deleteFile' , function (req , res ) {
  var url = 'http://localhost:8888/CompilerService?wsdl';
  var CodeID = req.body.CodeID;
  var args = {
    CodeID : CodeID,
    key : 'PFTYMbM7l4YkcJGtH9tcKfBrx0fyEaGD'
  };
  soap.createClient(url, function(err, client) {
    client.DeleteCode(args, function(err, result) {
      console.log(result.DeleteCodeResult);
;      res.send(result.DeleteCodeResult);
    });
  });

});
app.post('/updateFile' , function (req , res ) {
  
  var url = 'http://localhost:8888/CompilerService?wsdl';
  var CodeID = req.body.CodeID;
  var code = req.body.code;
  var args = {
    CodeID : CodeID,
    code : code,
    key : 'PFTYMbM7l4YkcJGtH9tcKfBrx0fyEaGD'
  };
  soap.createClient(url, function(err, client) {
    client.UpdateCode(args, function(err, result) {
      console.log(result.DeleteCodeResult);
;      res.send(result.UpdateCodeResult);
    });
  });

});