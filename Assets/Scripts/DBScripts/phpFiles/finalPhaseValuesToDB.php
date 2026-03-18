<?php

// $db_host = 'sql7.freemysqlhosting.net';
// $db_user = 'sql7547540';
// $db_password = 'yBFAq7dttq';
// $db_db = 'sql7547540';
// $db_port = 3306;

// $mysqli = new mysqli($db_host, $db_user, $db_password, $db_db, $db_port);

// if ($mysqli->connect_error) {
//     echo 'Errno: ' . $mysqli->connect_errno;
//     echo '<br>';
//     echo 'Error: ' . $mysqli->connect_error;
//     exit();
// }

$path = __DIR__ . '/DB_Connections.php';
include $path;

$mysqli = OpenCon();


echo 'Success: A proper connection to MySQL was made.';
echo '<br>';
echo 'Host information: ' . $mysqli->host_info;
echo '<br>';
echo 'Protocol version: ' . $mysqli->protocol_version;


$playerid = $_POST['playerID'];
$PhaseCount = intval($_POST['PhaseCount']);

//for methods



if ($PhaseCount == 1) {
    $method1 = $_POST['method_1'];
    $method2 = $_POST['method_2'];
    $method3 = $_POST['method_3'];

    $insertuserquery = "UPDATE investments SET method_1 = '" . $method1 . "', method_2 = '" . $method2 . "', method_3 = '" . $method3 . "' WHERE player_id = '" . $playerid . "'";
} elseif ($PhaseCount == 2) {
    $method1 = $_POST['method_1'];
    $method2 = $_POST['method_2'];
    $method3 = $_POST['method_3'];
    $method4 = $_POST['method_4'];
    $method5 = $_POST['method_5'];

    $insertuserquery = "UPDATE investments SET method_1 = '" . $method1 . "', method_2 = '" . $method2 . "', method_3 = '" . $method3 . "', method_4 = '" . $method4 . "', method_5 = '" . $method5 . "'  WHERE player_id = '" . $playerid . "'";
} elseif ($PhaseCount == 3) {
    // $insertuserquery = "UPDATE investments SET method_1 = '" . $method1 . "', method_2 = '" . $method2 . "', method_3 = '" . $method3 . "' WHERE player_id = '" . $playerid . "'";
    $method1 = $_POST['method_1'];
    $method2 = $_POST['method_2'];
    $method3 = $_POST['method_3'];
    $method4 = $_POST['method_4'];
    $method5 = $_POST['method_5'];
    $method6 = $_POST['method_6'];
    $method7 = $_POST['method_7'];

    $insertuserquery = "UPDATE investments SET method_1 = '" . $method1 . "', method_2 = '" . $method2 . "', method_3 = '" . $method3 . "', method_4 = '" . $method4 . "', method_5 = '" . $method5 . "', method_6 = '" . $method6 . "', method_7 = '" . $method7 . "' WHERE player_id = '" . $playerid . "'";

} elseif ($PhaseCount == 4) {
    // $insertuserquery = "UPDATE investments SET method_1 = '" . $method1 . "', method_2 = '" . $method2 . "', method_3 = '" . $method3 . "' WHERE player_id = '" . $playerid . "'";
    $method1 = $_POST['method_1'];
    $method2 = $_POST['method_2'];
    $method3 = $_POST['method_3'];
    $method4 = $_POST['method_4'];
    $method5 = $_POST['method_5'];
    $method6 = $_POST['method_6'];
    $method7 = $_POST['method_7'];
    $method8 = $_POST['method_8'];
    $method9 = $_POST['method_9'];

    $insertuserquery = "UPDATE investments SET method_1 = '" . $method1 . "', method_2 = '" . $method2 . "', method_3 = '" . $method3 . "', method_4 = '" . $method4 . "', method_5 = '" . $method5 . "', method_6 = '" . $method6 . "', method_7 = '" . $method7 . "', method_8 = '" . $method8 . "', method_9 = '" . $method9 . "' WHERE player_id = '" . $playerid . "'";
}



if ($mysqli->query($insertuserquery)) {
    echo 'Records Updated';
} else {
    echo $mysqli->error;
}


// CloseCon($mysqli);
