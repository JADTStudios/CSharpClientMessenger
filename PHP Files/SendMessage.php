<?php

$key = $_GET['key'];
$user = $_GET['username'];
$message = $_GET['message'];

$date = date("Y/m/d");
$file = "msg.txt";

$validkey = "0123456789";

if (is_numeric($key))
{
    if ($key == $validkey)
    {
        if (isset($key))
        {
            
        } else {
            die("Unknown Error!");
        }
    } else {
        die("Invalid Key!");
    }
} else {
    die("Key must not be empty and must contain only numbers!");
}

if(is_file($file))
{

    $file = fopen($file, 'a+') or die("Error!");
    echo("Message Sent!");
    fwrite($file, "\n   [$date] $user: $message");

}
else
{

    $file = fopen($file, 'w') or die("Error!");
    fwrite($file, "   [$date] $user: $message");
    echo("Message Sent!");
    fclose($file);
}
?>