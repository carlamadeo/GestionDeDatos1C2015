--PROCEDIMIENTOS
DROP PROCEDURE SQL_SERVANT.sp_login_check_valid_user

DROP PROCEDURE SQL_SERVANT.sp_login_get_answer_question_secret

DROP PROCEDURE SQL_SERVANT.sp_password_check_ok

DROP PROCEDURE SQL_SERVANT.sp_password_change

DROP PROCEDURE SQL_SERVANT.sp_login_check_password

DROP PROCEDURE SQL_SERVANT.sp_save_auditoria_login

DROP PROCEDURE SQL_SERVANT.sp_check_user_is_admin

DROP PROCEDURE SQL_SERVANT.sp_menu_list_functionality_by_user

DROP PROCEDURE SQL_SERVANT.check_and_update_accounts

DROP PROCEDURE SQL_SERVANT.sp_rol_exist_one_by_user

DROP PROCEDURE SQL_SERVANT.sp_rol_search

DROP PROCEDURE SQL_SERVANT.sp_rol_create

DROP PROCEDURE SQL_SERVANT.sp_rol_functionality_availability

DROP PROCEDURE SQL_SERVANT.sp_rol_functionality_enabled

DROP PROCEDURE SQL_SERVANT.sp_rol_functionality_add

DROP PROCEDURE SQL_SERVANT.sp_rol_functionality_remove

DROP PROCEDURE SQL_SERVANT.sp_rol_is_enabled

DROP PROCEDURE SQL_SERVANT.sp_user_search

DROP PROCEDURE SQL_SERVANT.sp_user_enable_disable

DROP PROCEDURE SQL_SERVANT.sp_user_get_by_user

DROP PROCEDURE SQL_SERVANT.sp_user_save_update

DROP PROCEDURE SQL_SERVANT.sp_user_clean_login

DROP PROCEDURE SQL_SERVANT.sp_user_get_user_rol

DROP PROCEDURE SQL_SERVANT.sp_user_get_not_user_rol

DROP PROCEDURE SQL_SERVANT.sp_user_rol_add

DROP PROCEDURE SQL_SERVANT.sp_user_rol_remove

DROP PROCEDURE SQL_SERVANT.sp_consulta_check_account

DROP PROCEDURE SQL_SERVANT.sp_consulta_saldo

DROP PROCEDURE SQL_SERVANT.sp_consulta_last_5_deposits

DROP PROCEDURE SQL_SERVANT.sp_consulta_last_5_withdrawal

DROP PROCEDURE SQL_SERVANT.sp_consulta_last_10_transfers

DROP PROCEDURE SQL_SERVANT.sp_estadistic_top_5_country_movement

DROP PROCEDURE SQL_SERVANT.sp_estadistic_top_5_cli_more_pay_commission

DROP PROCEDURE SQL_SERVANT.sp_estadistic_top_5_cli_trans_own_count

DROP PROCEDURE SQL_SERVANT.sp_estadistic_top_5_client_disable_account

DROP PROCEDURE SQL_SERVANT.sp_estadistic_top_5_count_type_payment

DROP PROCEDURE SQL_SERVANT.sp_account_search

DROP PROCEDURE SQL_SERVANT.sp_account_save_update

DROP PROCEDURE SQL_SERVANT.sp_account_get_data

DROP PROCEDURE SQL_SERVANT.sp_account_enabled_with_credit_search

DROP PROCEDURE SQL_SERVANT.sp_client_search_by_lastname

DROP PROCEDURE SQL_SERVANT.sp_client_search

DROP PROCEDURE SQL_SERVANT.sp_client_enable_disable

DROP PROCEDURE SQL_SERVANT.sp_client_data_get_by_id_client

DROP PROCEDURE SQL_SERVANT.sp_client_nro_identity_is_valid

DROP PROCEDURE SQL_SERVANT.sp_client_check_exist_identification

DROP PROCEDURE SQL_SERVANT.sp_client_check_exist_mail

DROP PROCEDURE SQL_SERVANT.sp_client_save_update

DROP PROCEDURE SQL_SERVANT.sp_client_tarjeta_disable

DROP PROCEDURE SQL_SERVANT.sp_client_is_enabled

DROP PROCEDURE SQL_SERVANT.sp_client_get_by_user

DROP PROCEDURE SQL_SERVANT.sp_client_tarjeta_get_by_id_client

DROP PROCEDURE SQL_SERVANT.sp_card_by_client_id

DROP PROCEDURE SQL_SERVANT.sp_card_associate

DROP PROCEDURE SQL_SERVANT.sp_card_get_data

DROP PROCEDURE SQL_SERVANT.sp_card_save_with_association

DROP PROCEDURE SQL_SERVANT.sp_card_exist

DROP PROCEDURE SQL_SERVANT.sp_card_enabled_search

DROP PROCEDURE SQL_SERVANT.sp_tarjeta_save

DROP PROCEDURE SQL_SERVANT.sp_tarjeta_not_expired

DROP PROCEDURE SQL_SERVANT.sp_retirement_generate_extraction

DROP PROCEDURE SQL_SERVANT.sp_save_deposito

--DROP PROCEDURE SQL_SERVANT.sp_get_tipo_moneda

DROP PROCEDURE SQL_SERVANT.sp_get_importe_maximo_por_cuenta

DROP PROCEDURE SQL_SERVANT.sp_check_account_same_client

DROP PROCEDURE SQL_SERVANT.sp_save_transferencia

DROP PROCEDURE SQL_SERVANT.sp_remove_pending_facturacion

DROP PROCEDURE SQL_SERVANT.sp_create_factura

DROP PROCEDURE SQL_SERVANT.sp_create_facturacion_item

--FUNCIONES
DROP FUNCTION SQL_SERVANT.Crear_Nombre_Usuario

DROP FUNCTION SQL_SERVANT.Validar_Tarjeta_Habilitacion

DROP FUNCTION SQL_SERVANT.Es_Fecha_Anterior

--CON FOREIGN KEY
DROP TABLE SQL_SERVANT.Facturacion_Item

DROP TABLE SQL_SERVANT.Facturacion

DROP TABLE SQL_SERVANT.Facturacion_Pendiente

DROP TABLE SQL_SERVANT.Auditoria_Login

DROP TABLE SQL_SERVANT.Transferencia

DROP TABLE SQL_SERVANT.Cheque_Retiro

DROP TABLE SQL_SERVANT.Rol_Funcionalidad

DROP TABLE SQL_SERVANT.Usuario_Rol

DROP TABLE SQL_SERVANT.Usuario_Cliente

DROP TABLE SQL_SERVANT.Periodo_Tipo_Cuenta

DROP TABLE SQL_SERVANT.Costo_Tipo_Cuenta

DROP TABLE SQL_SERVANT.Deposito

DROP TABLE SQL_SERVANT.Cliente_Cuenta

DROP TABLE SQL_SERVANT.Cliente_Tarjeta

DROP TABLE SQL_SERVANT.Tarjeta

DROP TABLE SQL_SERVANT.Log

DROP TABLE SQL_SERVANT.Cliente

DROP TABLE SQL_SERVANT.Cliente_Datos

DROP TABLE SQL_SERVANT.Cheque

DROP TABLE SQL_SERVANT.Retiro

DROP TABLE SQL_SERVANT.Cuenta

DROP TABLE SQL_SERVANT.Tipo_Item

--SIN FOREIGN KEY
DROP TABLE SQL_SERVANT.Banco

DROP TABLE SQL_SERVANT.Pais

DROP TABLE SQL_SERVANT.Moneda

DROP TABLE SQL_SERVANT.Estado_Cuenta

DROP TABLE SQL_SERVANT.Tipo_Cuenta

DROP TABLE SQL_SERVANT.Funcionalidad

DROP TABLE SQL_SERVANT.Tarjeta_Empresa

DROP TABLE SQL_SERVANT.Tipo_Identificacion

DROP TABLE SQL_SERVANT.Rol

DROP TABLE SQL_SERVANT.Usuario

DROP TABLE SQL_SERVANT.Estadistica

DROP TABLE SQL_SERVANT.Ano

DROP TABLE SQL_SERVANT.Trimestre

DROP TABLE SQL_SERVANT.Motivo_Log

DROP SCHEMA SQL_SERVANT