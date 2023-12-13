--
-- PostgreSQL database dump
--

-- Dumped from database version 15.4
-- Dumped by pg_dump version 15.3

-- Started on 2023-12-13 09:11:00

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- TOC entry 8 (class 2615 OID 2200)
-- Name: public; Type: SCHEMA; Schema: -; Owner: azure_pg_admin
--

CREATE SCHEMA public;


ALTER SCHEMA public OWNER TO azure_pg_admin;

--
-- TOC entry 4041 (class 0 OID 0)
-- Dependencies: 8
-- Name: SCHEMA public; Type: COMMENT; Schema: -; Owner: azure_pg_admin
--

COMMENT ON SCHEMA public IS 'standard public schema';


SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- TOC entry 231 (class 1259 OID 32812)
-- Name: account; Type: TABLE; Schema: public; Owner: ARDZAdmin
--

CREATE TABLE public.account (
    id integer NOT NULL,
    email character varying(48) NOT NULL,
    phone character varying(15),
    address character varying(128),
    verified boolean,
    closed boolean,
    username character varying(20)
);


ALTER TABLE public.account OWNER TO "ARDZAdmin";

--
-- TOC entry 230 (class 1259 OID 32811)
-- Name: account_id_seq; Type: SEQUENCE; Schema: public; Owner: ARDZAdmin
--

ALTER TABLE public.account ALTER COLUMN id ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public.account_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- TOC entry 241 (class 1259 OID 32914)
-- Name: cart; Type: TABLE; Schema: public; Owner: ARDZAdmin
--

CREATE TABLE public.cart (
    id integer NOT NULL,
    quantity integer,
    actualprice money,
    itemid integer,
    accountid integer
);


ALTER TABLE public.cart OWNER TO "ARDZAdmin";

--
-- TOC entry 240 (class 1259 OID 32913)
-- Name: cart_id_seq; Type: SEQUENCE; Schema: public; Owner: ARDZAdmin
--

ALTER TABLE public.cart ALTER COLUMN id ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public.cart_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- TOC entry 223 (class 1259 OID 32771)
-- Name: filetype; Type: TABLE; Schema: public; Owner: ARDZAdmin
--

CREATE TABLE public.filetype (
    id integer NOT NULL,
    fileextension character varying(6) NOT NULL
);


ALTER TABLE public.filetype OWNER TO "ARDZAdmin";

--
-- TOC entry 222 (class 1259 OID 32770)
-- Name: filetype_id_seq; Type: SEQUENCE; Schema: public; Owner: ARDZAdmin
--

ALTER TABLE public.filetype ALTER COLUMN id ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public.filetype_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- TOC entry 229 (class 1259 OID 32794)
-- Name: item; Type: TABLE; Schema: public; Owner: ARDZAdmin
--

CREATE TABLE public.item (
    id integer NOT NULL,
    itemname character varying(32) NOT NULL,
    description character varying(512) NOT NULL,
    pictureid integer,
    pricebase money NOT NULL,
    statusid integer
);


ALTER TABLE public.item OWNER TO "ARDZAdmin";

--
-- TOC entry 228 (class 1259 OID 32793)
-- Name: item_id_seq; Type: SEQUENCE; Schema: public; Owner: ARDZAdmin
--

ALTER TABLE public.item ALTER COLUMN id ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public.item_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- TOC entry 239 (class 1259 OID 32876)
-- Name: itemreview; Type: TABLE; Schema: public; Owner: ARDZAdmin
--

CREATE TABLE public.itemreview (
    id integer NOT NULL,
    itemid integer,
    accountid integer,
    rating integer NOT NULL,
    reviewdate timestamp without time zone NOT NULL,
    reviewtext character varying(1024) NOT NULL,
    CONSTRAINT itemreview_rating_check CHECK (((rating >= 1) AND (rating <= 5)))
);


ALTER TABLE public.itemreview OWNER TO "ARDZAdmin";

--
-- TOC entry 238 (class 1259 OID 32875)
-- Name: itemreview_id_seq; Type: SEQUENCE; Schema: public; Owner: ARDZAdmin
--

ALTER TABLE public.itemreview ALTER COLUMN id ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public.itemreview_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- TOC entry 235 (class 1259 OID 32829)
-- Name: orderitem; Type: TABLE; Schema: public; Owner: ARDZAdmin
--

CREATE TABLE public.orderitem (
    id integer NOT NULL,
    itemid integer,
    orderid integer,
    quantity integer NOT NULL,
    pricepaid money NOT NULL
);


ALTER TABLE public.orderitem OWNER TO "ARDZAdmin";

--
-- TOC entry 234 (class 1259 OID 32828)
-- Name: orderitem_id_seq; Type: SEQUENCE; Schema: public; Owner: ARDZAdmin
--

ALTER TABLE public.orderitem ALTER COLUMN id ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public.orderitem_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- TOC entry 225 (class 1259 OID 32777)
-- Name: picture; Type: TABLE; Schema: public; Owner: ARDZAdmin
--

CREATE TABLE public.picture (
    id integer NOT NULL,
    filetypeid integer,
    imagename character varying(100) NOT NULL
);


ALTER TABLE public.picture OWNER TO "ARDZAdmin";

--
-- TOC entry 224 (class 1259 OID 32776)
-- Name: picture_id_seq; Type: SEQUENCE; Schema: public; Owner: ARDZAdmin
--

ALTER TABLE public.picture ALTER COLUMN id ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public.picture_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- TOC entry 233 (class 1259 OID 32818)
-- Name: purchaseorder; Type: TABLE; Schema: public; Owner: ARDZAdmin
--

CREATE TABLE public.purchaseorder (
    id integer NOT NULL,
    accountid integer,
    notes character varying(256),
    orderdate timestamp without time zone NOT NULL
);


ALTER TABLE public.purchaseorder OWNER TO "ARDZAdmin";

--
-- TOC entry 232 (class 1259 OID 32817)
-- Name: purchaseorder_id_seq; Type: SEQUENCE; Schema: public; Owner: ARDZAdmin
--

ALTER TABLE public.purchaseorder ALTER COLUMN id ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public.purchaseorder_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- TOC entry 237 (class 1259 OID 32845)
-- Name: review; Type: TABLE; Schema: public; Owner: ARDZAdmin
--

CREATE TABLE public.review (
    id integer NOT NULL,
    rating integer NOT NULL,
    accountid integer,
    reviewbody character varying(256) NOT NULL,
    reviewdate timestamp without time zone NOT NULL,
    CONSTRAINT review_rating_check CHECK (((rating >= 1) AND (rating <= 5)))
);


ALTER TABLE public.review OWNER TO "ARDZAdmin";

--
-- TOC entry 236 (class 1259 OID 32844)
-- Name: review_id_seq; Type: SEQUENCE; Schema: public; Owner: ARDZAdmin
--

ALTER TABLE public.review ALTER COLUMN id ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public.review_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- TOC entry 227 (class 1259 OID 32788)
-- Name: status; Type: TABLE; Schema: public; Owner: ARDZAdmin
--

CREATE TABLE public.status (
    id integer NOT NULL,
    status character varying(16)
);


ALTER TABLE public.status OWNER TO "ARDZAdmin";

--
-- TOC entry 226 (class 1259 OID 32787)
-- Name: status_id_seq; Type: SEQUENCE; Schema: public; Owner: ARDZAdmin
--

ALTER TABLE public.status ALTER COLUMN id ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public.status_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- TOC entry 4025 (class 0 OID 32812)
-- Dependencies: 231
-- Data for Name: account; Type: TABLE DATA; Schema: public; Owner: ARDZAdmin
--

COPY public.account (id, email, phone, address, verified, closed, username) FROM stdin;
1	username@gmail.com	8018675309	3035 s 400 e Ephraim UT, 84627	t	f	user
2	fluffiness@gmail.com	4358675309	null	f	t	stranger
3	frickinusersman@gmail.com	\N	2819 n 200 e Ephram UT, 84627	f	f	engineer
4	zanerclegg@gmail.com	8015736090	\N	t	\N	ZaneClegg
\.


--
-- TOC entry 4035 (class 0 OID 32914)
-- Dependencies: 241
-- Data for Name: cart; Type: TABLE DATA; Schema: public; Owner: ARDZAdmin
--

COPY public.cart (id, quantity, actualprice, itemid, accountid) FROM stdin;
\.


--
-- TOC entry 4017 (class 0 OID 32771)
-- Dependencies: 223
-- Data for Name: filetype; Type: TABLE DATA; Schema: public; Owner: ARDZAdmin
--

COPY public.filetype (id, fileextension) FROM stdin;
1	.bin
2	.txt
3	.png
4	.jpeg
7	.jpg
\.


--
-- TOC entry 4023 (class 0 OID 32794)
-- Dependencies: 229
-- Data for Name: item; Type: TABLE DATA; Schema: public; Owner: ARDZAdmin
--

COPY public.item (id, itemname, description, pictureid, pricebase, statusid) FROM stdin;
7	Single Winchester Gold	Single 12g Winchester gold	33	$15.00	9
8	Double Winchester Blue	Double 12g & 20g Winchester Blue	34	$20.00	9
9	Double Remington Blue	Double 12g & 20g Remington Blue	35	$20.00	9
10	Single Winchester silver	Single 12g Winchester silver	36	$15.00	9
11	Double Winchester plain	Double 12g & 20g Winchester plain	37	$20.00	9
12	Single Remington Blue	12g Remington single silver blue	38	$15.00	9
13	Silver/Turquoise Hoops	Silver/turquoise hoops	41	$20.00	9
14	Custom Sign Small	Custom wood signs	39	$20.00	9
15	Custom Sign Medium	Custom wood signs	39	$35.00	9
16	Custom Sign Large	Custom wood signs	39	$50.00	9
17	Wood Cutout Small	Wood cutouts	40	$8.00	9
18	Wood Cutout Medium	Wood cutouts	40	$15.00	9
19	Wood Cutout Large	Wood cutouts	40	$20.00	9
\.


--
-- TOC entry 4033 (class 0 OID 32876)
-- Dependencies: 239
-- Data for Name: itemreview; Type: TABLE DATA; Schema: public; Owner: ARDZAdmin
--

COPY public.itemreview (id, itemid, accountid, rating, reviewdate, reviewtext) FROM stdin;
1	7	1	1	2022-10-19 10:23:54	Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod.
2	8	2	2	2023-10-19 10:23:54	Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod.
3	9	3	3	2023-10-19 10:23:54	Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod.
4	10	1	4	2018-10-19 10:23:54	Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod.
5	11	2	5	2017-10-19 10:23:54	Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod.
\.


--
-- TOC entry 4029 (class 0 OID 32829)
-- Dependencies: 235
-- Data for Name: orderitem; Type: TABLE DATA; Schema: public; Owner: ARDZAdmin
--

COPY public.orderitem (id, itemid, orderid, quantity, pricepaid) FROM stdin;
2	7	1	1	$15.00
3	8	2	2	$40.00
4	9	3	3	$50.00
5	10	1	4	$55.00
6	11	2	5	$100.00
\.


--
-- TOC entry 4019 (class 0 OID 32777)
-- Dependencies: 225
-- Data for Name: picture; Type: TABLE DATA; Schema: public; Owner: ARDZAdmin
--

COPY public.picture (id, filetypeid, imagename) FROM stdin;
39	7	customSign1
40	7	woodCutouts1
41	7	turquoiseHoops
33	7	12WinchesterGold
34	7	1220WinchesterBlue
35	7	1220RemingtonBlue
36	7	12WinchesterSilver
37	7	1220WinchesterPlain
38	7	12RemingtonBlue
\.


--
-- TOC entry 4027 (class 0 OID 32818)
-- Dependencies: 233
-- Data for Name: purchaseorder; Type: TABLE DATA; Schema: public; Owner: ARDZAdmin
--

COPY public.purchaseorder (id, accountid, notes, orderdate) FROM stdin;
1	1	\N	2022-10-19 10:23:54
2	2	weird guy	2022-10-19 10:23:54
3	3	notes notes notes	2022-10-19 10:23:54
4	1	stuff	2022-10-19 10:23:54
5	2	weird person	2022-10-19 10:23:54
\.


--
-- TOC entry 4031 (class 0 OID 32845)
-- Dependencies: 237
-- Data for Name: review; Type: TABLE DATA; Schema: public; Owner: ARDZAdmin
--

COPY public.review (id, rating, accountid, reviewbody, reviewdate) FROM stdin;
1	1	1	Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod.	2022-10-19 10:23:54
2	2	2	Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod.	2022-10-19 10:23:54
3	3	3	Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod.	2023-10-19 10:23:54
4	4	1	Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod.	2023-11-19 10:23:54
5	5	2	Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod.	2023-12-19 10:23:54
\.


--
-- TOC entry 4021 (class 0 OID 32788)
-- Dependencies: 227
-- Data for Name: status; Type: TABLE DATA; Schema: public; Owner: ARDZAdmin
--

COPY public.status (id, status) FROM stdin;
9	AVAILABLE
10	OUTOFSTOCK
11	DISCONTINUED
12	PREVIEW
\.


--
-- TOC entry 4042 (class 0 OID 0)
-- Dependencies: 230
-- Name: account_id_seq; Type: SEQUENCE SET; Schema: public; Owner: ARDZAdmin
--

SELECT pg_catalog.setval('public.account_id_seq', 3, true);


--
-- TOC entry 4043 (class 0 OID 0)
-- Dependencies: 240
-- Name: cart_id_seq; Type: SEQUENCE SET; Schema: public; Owner: ARDZAdmin
--

SELECT pg_catalog.setval('public.cart_id_seq', 1, false);


--
-- TOC entry 4044 (class 0 OID 0)
-- Dependencies: 222
-- Name: filetype_id_seq; Type: SEQUENCE SET; Schema: public; Owner: ARDZAdmin
--

SELECT pg_catalog.setval('public.filetype_id_seq', 7, true);


--
-- TOC entry 4045 (class 0 OID 0)
-- Dependencies: 228
-- Name: item_id_seq; Type: SEQUENCE SET; Schema: public; Owner: ARDZAdmin
--

SELECT pg_catalog.setval('public.item_id_seq', 19, true);


--
-- TOC entry 4046 (class 0 OID 0)
-- Dependencies: 238
-- Name: itemreview_id_seq; Type: SEQUENCE SET; Schema: public; Owner: ARDZAdmin
--

SELECT pg_catalog.setval('public.itemreview_id_seq', 5, true);


--
-- TOC entry 4047 (class 0 OID 0)
-- Dependencies: 234
-- Name: orderitem_id_seq; Type: SEQUENCE SET; Schema: public; Owner: ARDZAdmin
--

SELECT pg_catalog.setval('public.orderitem_id_seq', 6, true);


--
-- TOC entry 4048 (class 0 OID 0)
-- Dependencies: 224
-- Name: picture_id_seq; Type: SEQUENCE SET; Schema: public; Owner: ARDZAdmin
--

SELECT pg_catalog.setval('public.picture_id_seq', 41, true);


--
-- TOC entry 4049 (class 0 OID 0)
-- Dependencies: 232
-- Name: purchaseorder_id_seq; Type: SEQUENCE SET; Schema: public; Owner: ARDZAdmin
--

SELECT pg_catalog.setval('public.purchaseorder_id_seq', 6, true);


--
-- TOC entry 4050 (class 0 OID 0)
-- Dependencies: 236
-- Name: review_id_seq; Type: SEQUENCE SET; Schema: public; Owner: ARDZAdmin
--

SELECT pg_catalog.setval('public.review_id_seq', 5, true);


--
-- TOC entry 4051 (class 0 OID 0)
-- Dependencies: 226
-- Name: status_id_seq; Type: SEQUENCE SET; Schema: public; Owner: ARDZAdmin
--

SELECT pg_catalog.setval('public.status_id_seq', 12, true);


--
-- TOC entry 3852 (class 2606 OID 32816)
-- Name: account account_pkey; Type: CONSTRAINT; Schema: public; Owner: ARDZAdmin
--

ALTER TABLE ONLY public.account
    ADD CONSTRAINT account_pkey PRIMARY KEY (id);


--
-- TOC entry 3862 (class 2606 OID 32918)
-- Name: cart cart_pkey; Type: CONSTRAINT; Schema: public; Owner: ARDZAdmin
--

ALTER TABLE ONLY public.cart
    ADD CONSTRAINT cart_pkey PRIMARY KEY (id);


--
-- TOC entry 3844 (class 2606 OID 32775)
-- Name: filetype filetype_pkey; Type: CONSTRAINT; Schema: public; Owner: ARDZAdmin
--

ALTER TABLE ONLY public.filetype
    ADD CONSTRAINT filetype_pkey PRIMARY KEY (id);


--
-- TOC entry 3850 (class 2606 OID 32800)
-- Name: item item_pkey; Type: CONSTRAINT; Schema: public; Owner: ARDZAdmin
--

ALTER TABLE ONLY public.item
    ADD CONSTRAINT item_pkey PRIMARY KEY (id);


--
-- TOC entry 3860 (class 2606 OID 32883)
-- Name: itemreview itemreview_pkey; Type: CONSTRAINT; Schema: public; Owner: ARDZAdmin
--

ALTER TABLE ONLY public.itemreview
    ADD CONSTRAINT itemreview_pkey PRIMARY KEY (id);


--
-- TOC entry 3856 (class 2606 OID 32833)
-- Name: orderitem orderitem_pkey; Type: CONSTRAINT; Schema: public; Owner: ARDZAdmin
--

ALTER TABLE ONLY public.orderitem
    ADD CONSTRAINT orderitem_pkey PRIMARY KEY (id);


--
-- TOC entry 3846 (class 2606 OID 32781)
-- Name: picture picture_pkey; Type: CONSTRAINT; Schema: public; Owner: ARDZAdmin
--

ALTER TABLE ONLY public.picture
    ADD CONSTRAINT picture_pkey PRIMARY KEY (id);


--
-- TOC entry 3854 (class 2606 OID 32822)
-- Name: purchaseorder purchaseorder_pkey; Type: CONSTRAINT; Schema: public; Owner: ARDZAdmin
--

ALTER TABLE ONLY public.purchaseorder
    ADD CONSTRAINT purchaseorder_pkey PRIMARY KEY (id);


--
-- TOC entry 3858 (class 2606 OID 32850)
-- Name: review review_pkey; Type: CONSTRAINT; Schema: public; Owner: ARDZAdmin
--

ALTER TABLE ONLY public.review
    ADD CONSTRAINT review_pkey PRIMARY KEY (id);


--
-- TOC entry 3848 (class 2606 OID 32792)
-- Name: status status_pkey; Type: CONSTRAINT; Schema: public; Owner: ARDZAdmin
--

ALTER TABLE ONLY public.status
    ADD CONSTRAINT status_pkey PRIMARY KEY (id);


--
-- TOC entry 3872 (class 2606 OID 32924)
-- Name: cart cart_accountid_fkey; Type: FK CONSTRAINT; Schema: public; Owner: ARDZAdmin
--

ALTER TABLE ONLY public.cart
    ADD CONSTRAINT cart_accountid_fkey FOREIGN KEY (accountid) REFERENCES public.account(id);


--
-- TOC entry 3873 (class 2606 OID 32919)
-- Name: cart cart_itemid_fkey; Type: FK CONSTRAINT; Schema: public; Owner: ARDZAdmin
--

ALTER TABLE ONLY public.cart
    ADD CONSTRAINT cart_itemid_fkey FOREIGN KEY (itemid) REFERENCES public.item(id);


--
-- TOC entry 3864 (class 2606 OID 32801)
-- Name: item item_pictureid_fkey; Type: FK CONSTRAINT; Schema: public; Owner: ARDZAdmin
--

ALTER TABLE ONLY public.item
    ADD CONSTRAINT item_pictureid_fkey FOREIGN KEY (pictureid) REFERENCES public.picture(id);


--
-- TOC entry 3865 (class 2606 OID 32806)
-- Name: item item_statusid_fkey; Type: FK CONSTRAINT; Schema: public; Owner: ARDZAdmin
--

ALTER TABLE ONLY public.item
    ADD CONSTRAINT item_statusid_fkey FOREIGN KEY (statusid) REFERENCES public.status(id);


--
-- TOC entry 3870 (class 2606 OID 32889)
-- Name: itemreview itemreview_accountid_fkey; Type: FK CONSTRAINT; Schema: public; Owner: ARDZAdmin
--

ALTER TABLE ONLY public.itemreview
    ADD CONSTRAINT itemreview_accountid_fkey FOREIGN KEY (accountid) REFERENCES public.account(id);


--
-- TOC entry 3871 (class 2606 OID 32884)
-- Name: itemreview itemreview_itemid_fkey; Type: FK CONSTRAINT; Schema: public; Owner: ARDZAdmin
--

ALTER TABLE ONLY public.itemreview
    ADD CONSTRAINT itemreview_itemid_fkey FOREIGN KEY (itemid) REFERENCES public.item(id);


--
-- TOC entry 3867 (class 2606 OID 32834)
-- Name: orderitem orderitem_itemid_fkey; Type: FK CONSTRAINT; Schema: public; Owner: ARDZAdmin
--

ALTER TABLE ONLY public.orderitem
    ADD CONSTRAINT orderitem_itemid_fkey FOREIGN KEY (itemid) REFERENCES public.item(id);


--
-- TOC entry 3868 (class 2606 OID 32839)
-- Name: orderitem orderitem_orderid_fkey; Type: FK CONSTRAINT; Schema: public; Owner: ARDZAdmin
--

ALTER TABLE ONLY public.orderitem
    ADD CONSTRAINT orderitem_orderid_fkey FOREIGN KEY (orderid) REFERENCES public.purchaseorder(id);


--
-- TOC entry 3863 (class 2606 OID 32782)
-- Name: picture picture_filetypeid_fkey; Type: FK CONSTRAINT; Schema: public; Owner: ARDZAdmin
--

ALTER TABLE ONLY public.picture
    ADD CONSTRAINT picture_filetypeid_fkey FOREIGN KEY (filetypeid) REFERENCES public.filetype(id);


--
-- TOC entry 3866 (class 2606 OID 32896)
-- Name: purchaseorder purchaseorder_accountid_fkey; Type: FK CONSTRAINT; Schema: public; Owner: ARDZAdmin
--

ALTER TABLE ONLY public.purchaseorder
    ADD CONSTRAINT purchaseorder_accountid_fkey FOREIGN KEY (accountid) REFERENCES public.account(id) ON UPDATE CASCADE;


--
-- TOC entry 3869 (class 2606 OID 32851)
-- Name: review review_accountid_fkey; Type: FK CONSTRAINT; Schema: public; Owner: ARDZAdmin
--

ALTER TABLE ONLY public.review
    ADD CONSTRAINT review_accountid_fkey FOREIGN KEY (accountid) REFERENCES public.account(id);


-- Completed on 2023-12-13 09:11:09

--
-- PostgreSQL database dump complete
--

