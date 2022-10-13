<?php
header("Access-Control-Allow-Origin: *");

$servername = $_POST["serverName"];
$username = $_POST["userName"];
$password = $_POST["password"];

// Create connection
$conn = new mysqli($servername, $username, $password);

// Check connection
if ($conn->connect_error) {
  die($conn->connect_error);
}
echo "1";
?>