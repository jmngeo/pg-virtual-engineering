<?php

function OpenCon()
{
    // $dbhost = "localhost"; 
    // $dbuser = "dbuser";
    // $dbpass = "dbpass123@";
    // $db = "sql7547540";

    $dbhost = 'sql7.freemysqlhosting.net';
    $dbuser = 'sql7547540';
    $dbpass = 'yBFAq7dttq';
    $db = 'sql7547540';

    // $dbhost = 'localhost';
    // $dbuser = 'dbuser';
    // $dbpass = 'dbpass123@';
    // $db = 'sql7547540';

    $db_port = 3306;

    $conn = new mysqli("p:" . $dbhost, $dbuser, $dbpass, $db) or die("Connect failed: %s\n" . $conn->error);
    return $conn;
}
function CloseCon($conn)
{
    $conn->close();
}