<?php
  /* 
   * Paging
   */
   if (isset($_POST['id'])) {
        require 'connect.php';
			$query='DELETE FROM ElencoAccessi where ID=' .$_POST['id'];
			$result = mysql_query($query);

			// controllo l'esito
			if (!$result) {
				die("Errore nella query $query: " . mysql_error());		
			}

			if(0 == mysql_num_rows($result)) {
			echo 'Nessun accesso rilevato';
		}
		mysql_close();    
} 
	// recupero l'id autoincrement generato da MySQL per il nuovorecord inserito
	$id_inserito = mysql_insert_id();

	// chiudo la connessione a MySQL
	mysql_close();
?>