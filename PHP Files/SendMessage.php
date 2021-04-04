<?php

$user = $_GET['username'];
$message = $_GET['message'];
$date = date("Y/m/d");
$file = "msg.txt";

if(is_file($file))
{

    $file = fopen($file, 'a+') or die("Error!");
    fwrite($file, "\n$user: $message");

}else
{

    $file = fopen($file, 'w') or die("Error!");
    fwrite($file, "$user: $message");
    fclose($file);

}
?>