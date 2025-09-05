--==============================================================
-- Table: receivable_advance_request
--==============================================================
create table receivable_advance_request (
	receivable_advance_request_uid VARCHAR(36)          not null,
	creator_uuid         varchar(36)          not null,
	request_date         DATE                 not null,
	request_amount       DECIMAL(10,2)        not null,
	net_amount           DECIMAL(10,2)        not null,
	request_status_id    integer                  not null,
	finish_date 		 DATE				  null,
	primary key (receivable_advance_request_uid),
	foreign key (request_status_id)
	      references request_status (request_status_id)
);

--==============================================================
-- Table: request_status
--==============================================================
create table request_status (
	request_status_id    int                  not null,
	request_status_name  varchar(15)          not null,
	primary key (request_status_id)
);

--==============================================================
-- Index: ak_request_status
--==============================================================
create unique index ak_request_status on request_status (
	request_status_name ASC
);

insert into 
	request_status (request_status_id, request_status_name)
values
	(1, 'Pending'),
	(2, 'Approved'),
	(3, 'Rejected');
    