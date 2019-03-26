/*==============================================================*/
/* DBMS name:      MySQL 5.0                                    */
/* Created on:     2019/3/15 13:32:05                           */
/*==============================================================*/


drop table if exists byc_dailyReport_detail;

drop table if exists byc_project;

drop table if exists byc_report_task;

drop table if exists byc_task;

drop table if exists byc_taskDetail;

drop table if exists byx_dailyReport;

drop table if exists sys_dict;

drop table if exists sys_log;

drop table if exists sys_menu;

drop table if exists sys_role;

drop table if exists sys_role_menu;

drop table if exists sys_user;

drop table if exists sys_user_project;

drop table if exists sys_user_role;

/*==============================================================*/
/* Table: byc_dailyReport_detail                                */
/*==============================================================*/
create table byc_dailyReport_detail
(
   dd_id                int not null,
   dd_image             varchar(500),
   dd_file              varchar(500),
   dd_type              int,
   dd_status            int,
   dd_other             varchar(500),
   dd_cTime             datetime,
   dd_uTime             datetime,
   dd_sTime             datetime,
   dd_eTime             datetime,
   dr_id                int,
   dd_serious           bool,
   primary key (dd_id)
);

/*==============================================================*/
/* Table: byc_project                                           */
/*==============================================================*/
create table byc_project
(
   pr_id                int not null,
   pr_name              varchar(50),
   pr_status            int,
   pr_cTime             datetime,
   pr_uTime             datetime,
   pr_msg               varchar(1000),
   user_id              int,
   primary key (pr_id)
);

/*==============================================================*/
/* Table: byc_report_task                                       */
/*==============================================================*/
create table byc_report_task
(
   tk_id                int,
   dr_id                int
);

/*==============================================================*/
/* Table: byc_task                                              */
/*==============================================================*/
create table byc_task
(
   tk_id                int not null,
   tk_status            int,
   tk_title             varchar(100),
   pr_id                int,
   tk_msg               varchar(1),
   tk_priority          int,
   tk_cUser             int,
   tk_cTime             datetime,
   tk_tag               int,
   tk_sTime             datetime,
   tk_eTime             datetime,
   tk_Users             varchar(100),
   tk_executor          int,
   primary key (tk_id)
);

/*==============================================================*/
/* Table: byc_taskDetail                                        */
/*==============================================================*/
create table byc_taskDetail
(
   td_id                int not null,
   tk_id                int,
   td_content           varchar(1),
   td_type              int,
   td_cTime             datetime,
   td_cUser             int,
   primary key (td_id)
);

/*==============================================================*/
/* Table: byx_dailyReport                                       */
/*==============================================================*/
create table byx_dailyReport
(
   dr_id                int not null,
   dr_content           varchar(1),
   userORpro_id         int,
   dr_type              int,
   dr_status            int,
   dr_category          int,
   dr_image             varchar(200),
   dr_file              varchar(100),
   dr_ctime             datetime,
   dr_uTime             datetime,
   primary key (dr_id)
);

/*==============================================================*/
/* Table: sys_dict                                              */
/*==============================================================*/
create table sys_dict
(
   dc_id                int not null,
   dc_name              varchar(50),
   dc_type              varchar(30),
   dc_priority          int,
   dc_cTime             datetime,
   user_id              int,
   dc_uTime             datetime,
   isdelete             bool,
   primary key (dc_id)
);

/*==============================================================*/
/* Table: sys_log                                               */
/*==============================================================*/
create table sys_log
(
   log_id               int not null,
   log_type             int,
   log_grade            int,
   log_path             varchar(200),
   log_content          varchar(1),
   user_id              int,
   log_cTime            datetime,
   primary key (log_id)
);

/*==============================================================*/
/* Table: sys_menu                                              */
/*==============================================================*/
create table sys_menu
(
   menu_id              int not null,
   menu_name            varchar(20),
   menu_type            int,
   menu_link            varchar(50),
   menu_pid             int,
   user_id              int,
   menu_uTime           datetime,
   isDelete             bool,
   primary key (menu_id)
);

/*==============================================================*/
/* Table: sys_role                                              */
/*==============================================================*/
create table sys_role
(
   role_id              int not null,
   role_name            varchar(20),
   role_msg             varchar(200),
   user_id              int,
   role_ctime           datetime,
   role_uTime           datetime,
   is_delete            bool,
   primary key (role_id)
);

/*==============================================================*/
/* Table: sys_role_menu                                         */
/*==============================================================*/
create table sys_role_menu
(
   menu_id              int,
   role_id              int
);

/*==============================================================*/
/* Table: sys_user                                              */
/*==============================================================*/
create table sys_user
(
   user_id              int not null,
   user_name            varchar(20),
   user_pwd             varchar(20),
   name                 varchar(20),
   user_status          int,
   user_firstLogin      bool,
   user_cTime           datetime,
   user_lastLoginTime   datetime,
   primary key (user_id)
);

/*==============================================================*/
/* Table: sys_user_project                                      */
/*==============================================================*/
create table sys_user_project
(
   user_id              int,
   project_id           int,
   project_status       int
);

/*==============================================================*/
/* Table: sys_user_role                                         */
/*==============================================================*/
create table sys_user_role
(
   user_id              int,
   role_id              int
);

