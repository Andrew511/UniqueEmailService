# UniqueEmailService

This is a webservice written in C# capable of recieving a HTTPS POST request with a JSON body containing an array of email addresses.
  The webservice returns an integer indicating the number of unique email addresses. Uniqueness is checked with support for Gmail Account matching,
  with Gmail account matching, accounts such as abc+def@gmail.com and abc.@gmail.com will both correctly resolve to the main account abc@gmail.com.

This service also supports RFC requirements where email domain names are treated in a case insensitive manner, as well as RFC requirement RFC 5321 section 2.4,
  which specifies that the local mailbox must remain case sensitive.

When running this program, first run the "UniqueEmailService" application within the .zip file to start the web service listening on ports 5000 and 5001,
    then open a cmd.exe terminal and run the following command: 

curl -i -X POST -H "Content-Type:application/json" -d "[{\"RawEmail\": \"Jim@gmail.com\" }]" https://localhost:5001/api/UniqueEmail

Additional emails can be added to the array as additional JSON objects, invalid email formats will fail to validate and fail the request.
