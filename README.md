# EngMan
     url = http://*host*/api
     
     post /account/login
     post /account/registration 
     post /account/logout 
     get /account/getuserdata 
     get /account/getuserdictionary 
     delete /account/deletewordfromdictionary/ 
     post /account/addwordtodictionary/ 
     get /account/getallusers 
     delete /account/deleteuser/ 
     put /account/changerole 
     put /account/ChangePassword?id= 
     put /account/edituser 
     get /account/GetAllCategoriesOfDictionary 
     get /account/GetByCategoryDictionary?category= 
     get /rule/getallrules 
     get /rule/getrule/ 
     post /rule/addrule 
     put /rule/editrule 
     delete /rule/deleterule/ 
     get /rule/GetAllCategories 
     get /rule/GetByCategory?category= 
     post /rule/addimages 
     get /sentencetask/gettask?category=' + category + '&indexes= 
     post /sentencetask/verificationcorrectness 
     get /word/getwordmap?category=' + category + '&indexes=' + indexes + '&translate= 
     post /word/VerificationCorrectness?translate=' + translate 
     get /sentencetask/GetAllCategories 
     get /sentencetask/GetAllTasks 
     get /sentencetask/GetTaskById/ 
     post /sentencetask/AddTask 
     put /sentencetask/EditTask 
     delete /sentencetask/DeleteTask/ 
     get /sentencetask/GetByCategory?category= 
     get /word/GetAllCategories 
     get /word/GetAllWords 
     get /word/GetWordById/ 
     post /word/AddWord 
     put /word/EditWord 
     delete /word/DeleteWord/ 
     get /word/GetByCategory?category= 
     post /message/readmessages?senderId= 
     get /message/getallmessages/ 
     get /message/getnewmessages/ 
     get /message/getmessagesbyuserid?otherUserId=' + otherUserId + '&lastReceivedMessageId=' + lastReceivedMessageId 
     delete /message/deletemessage/ 
     post /message/SendMessage 
     get /guessestheimage/gettask?category=' + category + "&indexes= 
     post /guessestheimage/verificationcorrectness/ 
     get /guessestheimage/getalltasks/ 
     get /guessestheimage/gettaskbyid/ 
     put /guessestheimage/edittask/ 
     post /guessestheimage/addtask/ 
     delete /guessestheimage/deletetask/ 
     get /guessestheimage/GetAllCategories 
     get /guessestheimage/GetByCategory?category= 
     
     hubConnection('http://ecsc00a01a18')
     get new messages in real time: onUpdateMessages
     connect to server: Connect + model UserView
