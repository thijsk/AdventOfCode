﻿using Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp2019
{
    class Day6 : IDay
    {
        const string input = @"CYP)BC6
FPL)G1W
6MM)5MX
CXK)W4F
TW9)KG3
7LR)RVF
9LK)MJF
FZT)G7N
4F5)YJH
PJF)DJV
JYT)3WQ
839)Q21
8CW)NGC
564)8T3
M86)3NM
XSL)814
MH7)LDS
DKB)742
FWR)NDK
RFZ)B5P
MLZ)YZQ
3RK)FZB
X46)NQ1
Y64)VDS
H79)LPF
G4Q)HSZ
76X)37N
5X1)7PF
YT3)8RH
QPY)32L
3M3)XY4
Z9Z)ZP1
MZ1)H8X
2RY)VW7
8CB)L95
RQF)1NR
JRX)2N9
YV3)4R2
XKB)MFH
13H)1BW
WQS)XBR
WQ9)GQP
HGX)7G5
ZT8)7C6
DHD)HSD
TCX)7P7
8NF)KTM
FG4)V8D
C49)DJK
KBZ)MFF
9SG)DBC
YWQ)HF1
Y7T)7RN
6YM)BV6
B41)P1W
PSF)NLC
9VN)W8W
YS5)6RK
NLH)ZH9
S8D)2BJ
923)9RJ
VGK)D33
SL1)7LT
ZM4)S6L
G1P)QFG
RLW)6TK
NDK)DXV
DFN)7GM
B6Z)FL7
4JP)JCR
DQF)8CN
7PB)F3V
7QM)116
1WM)WS8
LLC)1YP
GP2)M37
DX1)V6G
VJZ)J4F
YPL)J14
712)2TM
37N)9RS
PV1)5F9
DJ7)125
TB3)JK3
YZQ)G13
NMV)M64
9WN)B6Z
Q62)3V6
3NM)N21
LBN)T51
KGL)TQ3
9P4)123
TRY)24T
K62)3QZ
D3K)MZD
Q33)GH3
8PZ)DRD
845)KZX
FH9)G1P
CT6)CZN
39L)S1T
GTP)HDP
NYH)VFT
Z7P)1B7
G87)ZPQ
M98)2PZ
T6K)YQB
KBP)8WY
Q6F)VZC
9Q7)DNQ
Y4T)C21
5VW)G32
K1S)Y64
7TW)QT6
TM2)LBM
GZ1)HBJ
K45)RPX
DGB)NF5
TZN)CQP
3M3)TJ1
STY)VH9
7KG)TSY
9QH)6ZW
TDN)TVJ
ZR4)KP8
D6Y)56K
4Q1)PQR
3X2)WVF
W4B)W6R
SWJ)4Q1
N6R)67W
R5F)15N
F17)GTS
7MF)H1Z
36V)KXV
131)JZC
5G3)KHV
J5R)HB2
2CD)LFV
MG5)YPV
859)1Y7
85D)6LW
TFP)5CJ
QZ2)JBW
C7L)86Y
1TJ)F5F
4GC)VM9
W6W)XV5
5LX)H82
4Q1)HL2
C9P)1CZ
SC8)WQS
78G)P4B
FF5)5PV
36J)YMC
8TM)QHV
793)XS6
P7F)TMJ
46W)DC2
YRZ)5F2
GHQ)5VW
7LY)W59
FZ8)SAN
W7K)R75
ZL8)HWH
DJV)X7C
MML)QCB
D5Y)R6R
2ZP)516
9KL)V35
P7P)HSN
H45)RTT
N6F)FWR
GB3)LL3
HPQ)LM3
2XS)6MM
YMC)HYL
X7B)Z2N
2ZD)217
D33)X7B
RKH)7TW
MMV)131
3TJ)L88
5F6)BJQ
MJ9)9FT
K4C)M99
NQM)HGQ
RQ2)SB9
J5G)KD3
X84)WF6
794)M77
SPD)844
XTC)RW1
7P7)KXQ
P4G)64X
BTR)57D
32Y)SJP
J3W)9QH
JTX)FPR
3NW)2ZP
8VC)PMP
PSL)MR8
H5F)BTR
8BT)73H
BSC)36B
F14)9SP
H1K)28T
36B)SJ6
8RH)RGP
JF3)J3W
SGC)DXB
CHY)94Y
RND)VJY
G23)CVN
MR9)FF5
SB9)F2N
NRK)9XQ
J14)8NS
RJ1)578
QG7)N41
R8L)XMZ
7SR)6W4
B7P)2X9
QRV)F9F
LMZ)FB7
F9M)DDH
HB2)H79
SM8)N1G
GMT)5F6
FL7)7MZ
CS8)CKK
CZV)89B
SZP)NJZ
N1G)H6Q
XV7)JX6
5L5)3C3
JCR)7V3
B9S)13H
123)S8B
RZ4)GR4
PV7)FMP
4RG)HX6
BMB)82H
RRK)KB3
FPR)DP3
826)8M2
Y1P)5KC
YSP)Y61
56T)MZ8
J8Q)HTW
Z4C)DTK
WBP)8WP
PN3)7KT
7KT)XK9
LDD)2TP
2X1)HZR
SZ9)WLP
FH9)Q9W
4L4)T4W
FRS)BMK
BTH)WQF
MX9)3ZQ
DG6)K6D
5TR)HD8
KV3)WW3
VCB)BJV
BMK)MX9
QT6)8VC
YDT)NH2
85Y)4JB
GXT)V4Z
7T6)XK4
8YR)SL1
9WL)MPZ
58H)H5Z
L7D)MG5
MNN)5GT
CVN)4M3
XCP)MZJ
JX6)BMB
F9M)VT3
246)3SW
1NX)MG7
6PG)SL3
1PR)8XF
RF7)W6W
VW7)S1D
HKB)C2Y
LH6)Y4T
DLN)4GC
N8R)YK3
QVZ)M3Q
WPW)9TS
PLX)PX8
F5P)FC6
DVZ)D5Y
91S)XCH
J2F)NSL
X9G)PN3
H8X)84H
MD9)C8Z
QNX)CWY
57D)YWX
MGF)7Y6
24W)V27
5WV)W7T
VRF)FPL
844)JVP
TWX)Z6B
63P)2T7
7C6)VR3
C1M)LKR
4ZP)BJG
9D4)69B
GQP)CTM
VCK)R59
HDB)CVL
WQH)M86
DRD)62D
VRR)KV3
XLD)5R2
B36)55B
F69)7T6
36B)NLJ
B5P)Q4Z
Y27)GDG
LHR)DG8
ZQQ)CKC
CYX)SMY
RBF)CBG
PRF)PRH
WW3)B28
DYL)2ZX
RGP)YNJ
DNQ)4R4
QCB)PBV
SK7)7QR
M4S)PJ5
D15)NX8
MZD)BRP
S1B)Q5H
M9V)VSJ
VGK)CGY
GC9)LKT
SDD)F31
KK3)LCB
1FF)4WC
87N)BVX
76M)1RN
ZS5)RND
374)VPW
QBW)X2Q
G51)63B
MC9)F35
T73)JTY
24V)QJN
MWN)S57
NHV)DHT
Y3K)D81
S6L)J5G
WSS)32Y
3SH)K74
SMY)JBJ
ZXP)212
Z85)RM5
HZC)9VN
6F6)8BT
845)KMW
JLZ)ZMW
NDB)1MY
7ZV)DG6
KNN)46W
34H)L5Q
87Q)J3K
4MP)Z7P
BJD)K2R
J31)R2N
585)7HL
3FP)JQS
PYG)Q1N
DC2)WGW
H95)4M1
QK3)13Y
6N4)T87
K4Q)626
HDF)FFQ
TX9)B6S
JTY)ZWR
94M)Y9F
G32)BHL
S4X)JF3
687)ZV9
QLX)T9F
BYK)2XC
PG4)XZ6
F7R)44V
7VL)QFR
DGG)CB1
498)ZS9
S67)X3Z
B22)68Q
G92)K3K
HG7)KBP
V8P)TZN
XZ6)TQD
8GG)TRY
HLF)FRS
YP6)X6X
PB2)R9Y
PCR)L8X
XNT)H22
HSD)Y54
TD1)NDB
ZMW)JYT
BB8)GSN
VN6)P6Q
63Y)QD2
QGJ)9ZM
XMZ)JS6
3BC)P7R
G13)MMC
DBK)96C
7WG)1F5
626)HYQ
96C)DFN
B39)NVP
QF9)6NZ
F3V)FVQ
JYS)GMT
523)SVM
RQ6)VF4
G7N)Z31
V8D)YRK
7GM)QS6
CGY)CBZ
7X4)D4K
HLS)V3C
VDS)39L
KNN)GC9
QS7)DZN
4C3)QX4
QTK)QVZ
RTS)8PZ
486)FZ8
NR7)HRL
GLJ)XLD
CWY)QV2
NH2)54M
WZ3)JZS
TFK)TQN
T5K)SF6
LPR)WB3
6WM)354
R9B)T37
26S)JJG
S24)TCZ
D5L)KSB
KVZ)YT3
7SL)F7R
8SJ)LQ2
5G6)C23
1RN)PSG
BVX)T8Z
X5N)G5W
Y1P)52M
KH1)2YC
RW1)ZY6
5CJ)7BB
WWS)VDF
C8Z)L5G
7QR)79Q
NSR)SXK
BRP)KNN
TJS)PRZ
BMB)T4X
7RG)K9C
HFP)HDB
NGC)B6T
BDR)HW3
NK7)JY9
71W)LWT
7G3)JVR
8M2)XWZ
RFB)MYL
HDP)7S5
WQQ)7QM
Q5H)NKM
ZRF)1XC
YNJ)D8S
M99)5G3
4PS)X1V
TCC)LQW
FD7)BTH
PX8)LSF
P9J)JQR
S15)C36
RNY)S4G
XS6)WCB
C5F)C7P
KKD)XR5
T7K)J3V
HKS)JPL
RK1)65J
DWY)479
D8S)4RG
DV2)T59
9TB)D26
V27)8NL
L2V)3M3
HX6)6H7
8NS)9P4
TFR)7HD
SJ6)TK2
TCC)7ZP
D3F)TMM
RK6)L17
RK6)HZC
BF7)ZTP
52M)1M7
MB3)S4B
L5J)3P8
VCB)MXP
S1T)T5P
DHS)NSR
131)4BV
VWR)R8L
K1D)9LK
796)QRV
CYZ)N31
MMC)HH6
LSF)YZJ
QS6)2ZS
DV9)K65
KT9)P27
CKK)6NB
V4Z)D6N
9WL)5J9
P88)HLF
TMM)7HN
XBQ)JG5
97N)81P
5VH)SDZ
MHF)QP6
NXM)3X2
B3L)ZQL
BM7)4F5
ZGZ)PQS
59F)F7C
6T3)59W
99C)K1X
BTK)4L4
M96)D9B
63Y)7KG
NF5)R4C
SZT)K5G
HZ1)KKD
C3J)J1S
Q21)NKR
1NC)CF2
SVJ)GJB
WNB)12X
QM3)RF4
HW3)VZV
RTT)DT8
7L5)62G
J4F)8DJ
LQW)77T
7GH)X51
33T)5JR
4LX)HC2
5JR)8CB
M5S)VT6
GR4)5HD
9X9)PLW
PQL)WWB
JY9)9SG
SJF)H9D
D9B)9JF
9XQ)BT2
RBD)JQY
5F9)TRS
5MT)XSL
XHP)MMW
HSN)17L
QCM)PGF
BVF)YBG
JS6)S1B
GZ5)B6L
YM2)8YR
P7R)NK7
J2B)J2F
9LF)S9G
4M4)Z9Z
7S5)KY9
M37)22M
QPV)Y1P
8CH)V9L
B6S)GTN
X6X)XLH
GKT)7RW
P6D)8W3
BJQ)Z4C
3ZQ)K1D
L57)BZR
XFX)Z5Y
94Y)F83
DXC)8W2
37S)GS4
ZTH)JX2
DP3)LYY
XBR)6JF
TTY)G51
VL3)SRQ
BNQ)5S8
62G)TMW
JK3)XWW
WDJ)KDJ
4M1)NGJ
Q1N)81K
G7R)GP2
LKT)7ZV
8NT)ZXP
KXQ)LSZ
12X)PD2
L1N)WZY
ZY6)TT4
7P7)6T1
Q2T)HDF
1HW)1CD
NTG)5BS
6G9)WVJ
F5P)GML
VJD)WWS
YVW)PM3
P2Y)ZZ4
JYK)L57
NN1)TW9
B69)YZZ
GRW)2XG
J4Q)HHB
K74)F25
55M)5LX
WR5)JTM
DDG)NRK
Y65)MC9
WVF)PSF
6CD)ZGZ
GSN)Y5X
BX1)ZHN
CLT)SPD
4R4)R2K
JPP)1GH
NKM)PY5
QH8)M8Y
JRX)6WM
Z3T)28Q
SVM)78G
XK9)4BW
ZR4)X9G
5B2)X6W
4BS)5NX
TT4)MGF
TQ1)M4Q
9KS)YWQ
W8W)M8T
QBT)CXK
N41)QLX
CNV)6PG
6T1)99C
125)V2S
QPD)QNX
H5Z)2KX
P9S)1XM
15N)4SM
FJ4)MNL
YPL)JDP
P8L)PLX
WVJ)SQC
V9L)G4Q
Y9L)K1S
1SS)F8X
9WR)F7H
167)15K
814)MSC
Z56)BSS
D5L)NHG
2R5)QGJ
GWT)LPR
ZM9)RWL
XK4)XVV
X2Q)HGG
RLY)2RY
837)7L4
7X4)ZL8
1Q3)1TJ
2KX)Z44
6W4)ZM1
C7J)8J1
17L)TD1
1F5)L2V
WDQ)ZK3
YY8)SFX
H82)4HJ
28T)D94
F5F)XMT
KP8)BZ8
116)9KS
GSK)K66
6QB)HD7
NGP)4X5
PRZ)TTY
WRZ)2YT
BJG)SGR
7ZF)TFK
1MY)826
825)PGZ
PJ5)9WR
PV1)XV7
516)85D
LQK)M96
B6L)LZK
2TM)DYR
FYH)SZ9
LKT)LTW
PDT)79D
WQQ)DYT
7PW)F81
KSB)CNV
BGN)XQJ
MC6)ZVC
2XC)M4S
F7B)PZ2
1YP)QVB
2FY)KT9
TQZ)S7J
3V6)9T9
CBG)XS5
R59)DLS
GT6)793
4BS)FT7
XY4)6LN
9VF)FH9
GSV)6PV
V4H)JLP
72K)CQS
DLN)ZVL
7RS)RQF
K2R)J94
M64)94M
5FD)FJX
5PV)R11
S4B)CVQ
66K)WBP
DZK)Z5S
MZJ)PCR
2FY)BYK
8J1)7RT
N31)GPX
7Y6)HQC
6CQ)W75
7BP)7SL
NCL)3FP
6JF)YPL
XJH)Z56
X6W)CHY
S27)5KS
96R)ZT8
P71)SGT
YQB)B8S
MZ8)DBK
2MY)NX5
GHK)N4C
5GT)YS5
P5K)1MP
96D)QM1
TVJ)PWV
VTR)X84
7HK)78Q
MQK)GBL
MXW)MWN
MNL)VMK
NHM)PQL
WXW)RXQ
B34)1NC
J9D)Y55
V8Z)PDT
MB7)24V
FTQ)8WG
VFT)XCY
22B)8NT
C4W)4ZP
JG5)WSW
PGF)15T
F7H)8CH
L2W)9HB
JLZ)F5P
SRV)HQ3
SLJ)TGQ
NJZ)C7L
M77)DHD
QVB)165
Z6B)QM3
HGQ)QB2
15C)M31
K1X)FKS
RXQ)G7Z
7TF)DKC
7G2)7PM
BNL)KGL
B52)VRR
81P)VGK
P61)LMP
FB2)LBH
Y3Z)5VH
LBD)X4F
H35)CSR
QFR)C9P
DQ9)54N
TJ2)5TR
93B)SP7
BX1)98H
1BQ)BSC
4WC)WR5
VT6)HR2
2BV)J4T
HVC)3F7
SNP)845
V6G)8CW
585)WC9
6GZ)C5Z
VWY)LVH
QV2)VL5
HNT)V4H
HX7)W7K
4HJ)BVF
MZ1)JY6
7DG)69G
LZK)673
V16)SJF
N8R)8XP
SQZ)BTC
63B)66P
MKY)SDD
TR2)6R9
DYT)5WV
V9L)CYX
SJP)R9V
ZTP)NJ6
XVV)DC7
SNP)VJZ
77Y)NYH
8G7)WDH
ZRX)HNB
B6G)QCM
Y82)V9B
KVL)FGP
CFQ)K4Q
T87)GHW
MJG)WKH
DDH)FNZ
86Y)CDJ
LBN)5HX
7JB)4Y3
KG3)8CC
2XG)Z95
CWH)3NW
NB7)MC6
DC2)6Y9
GWD)RPZ
GYH)JTX
PQR)6GT
7PF)F8G
BJV)D1M
GDN)MHF
54N)W4B
X1V)3LL
Y3Y)VWY
T17)Y65
BVR)H1K
H42)56T
22M)BW9
CS8)71W
KMW)143
WWS)RQ6
NMV)J4Q
58F)V8P
MPZ)Q33
DDX)Y3B
LPF)7HH
9RS)RLY
LQW)CWH
9Q7)ZZ9
5S8)96D
D7B)YFZ
8WY)HLS
4SM)P6M
WC9)ZTD
J94)N6F
7L1)2TC
YGY)XW6
HBQ)2X1
742)C49
DQ4)S4X
L8X)B39
WXY)YVW
JX3)YMS
ZWR)3RK
TDH)P14
58H)9BX
Y4Q)8Z6
VCK)KGY
P14)T6K
QB2)96R
P7B)FH8
9RR)2VJ
15T)TLN
75Z)GVQ
N8D)G92
G7Z)KR3
HTW)X7W
CTC)CYZ
59Q)HKB
87N)2BX
GH3)87W
663)RG4
DZW)797
ZNV)MYM
C1C)SM8
HYL)P88
SPB)CZV
S4G)S9M
D7T)J6J
3SW)S7Y
YS5)DDG
WJC)7SR
98H)885
CPT)BGN
GQD)SLJ
XQ7)Z2T
Y5W)55M
TFR)4PS
HQC)SQK
4M3)VPP
GCH)BZ9
644)G5N
MXP)JYN
CBZ)RB3
RGH)SJZ
GVQ)MVL
2WK)MR9
SL3)JRX
VT3)58F
G1C)J9C
KB5)C5D
VT6)SP9
578)ZTH
GBL)H69
M8Y)6DP
CTM)B69
28Q)7DG
H5K)WS7
1NR)62Y
F54)HPQ
GV6)V8Z
Y1D)D15
FFL)J6R
Z2N)7WG
R2N)M9V
21Y)FVT
7RS)5S4
Z19)KJY
W9C)JD5
NNM)NHT
24T)DGG
GB3)GV9
MYL)XF3
H9V)FFL
QM1)KVL
WBP)M7J
F7C)JWR
BWZ)LMZ
Z5Y)7S6
COM)HKS
XFT)GB3
HH6)XFX
R2X)KV7
5W2)F9M
4CK)RFB
71W)8GG
8XP)825
TLN)QXJ
WD9)TXX
R6R)WDY
4N7)DQ4
NKW)85Y
28N)4GR
SRQ)NR7
3SP)63P
99K)GHK
VZC)DYL
D7H)LQK
7HK)H1F
D6N)TG1
P82)C56
JX2)WD1
ZHN)CTR
5KC)S27
24D)7HW
77Y)V1H
N21)Y3Y
DJV)STY
5YQ)837
Z31)H4N
9FT)7PB
79Q)GKT
6JX)QH8
KR3)8ZQ
955)4CK
2X6)L78
ZMW)RBD
D5P)9WL
XMT)PFQ
5MX)KP2
8Z6)CT6
T59)NN1
CZG)N8R
171)712
5FJ)CND
YGY)RTS
6RK)5PY
LVL)P49
TPK)34H
S8L)59R
DXB)LDD
M8T)NCL
5RG)22B
F2N)DGB
W9Y)26L
N8F)VJD
Q4Z)R3P
V35)QZW
13Y)GL2
XZ6)RY8
W6R)9D4
R75)P71
3TX)9K6
D1M)XHP
HQ3)KG1
D4K)585
RGN)HG7
FJX)BVR
NVP)XSV
5BL)GZ5
TSY)PG4
ZV9)DLN
NVN)BTK
M31)QPD
VKS)66K
YPV)MQK
L5B)5JH
HL2)9VF
H9D)SWJ
J3S)XNB
T2J)YOU
3LL)7PW
H88)ZRF
NQ1)1SS
XFS)RW5
GPV)NZ3
TK2)RXV
CC8)VKQ
6FV)CVD
143)GSV
T5P)Y1D
7G5)7ZR
69B)Z85
YFZ)XKZ
MFH)8CK
4GR)PSL
2HK)5W2
J3K)7BY
ZP1)CQX
JGR)KVZ
199)75Z
VL3)5B2
N34)QG7
2T7)QPY
GDG)3TJ
S57)VTX
Y61)WXY
9ZK)JL4
ZJJ)J69
QNX)DJ7
SXK)Y82
R9Y)DDX
Z95)YZ1
CQP)PV1
1CD)24D
JQY)TDH
P9Y)WQ9
W87)69S
M7J)421
XKV)RJH
MG7)Y5W
BJV)GLJ
M37)RK7
HHB)5FD
LJ4)7Q5
Z5S)TCC
5NX)XYH
BZR)MQV
5HX)5DV
2CG)32G
X4F)N4V
DFZ)P7B
DVP)Q6F
PWV)TB3
S9G)TY2
J4T)CPT
LFF)YDT
TMW)L5B
HJJ)B2N
1G6)RFZ
K2R)HN4
PZ2)FRG
FB7)8SV
HZR)S8L
1GW)486
8W2)T97
SJ5)L1N
F8G)58H
C36)DFQ
ZDV)FG4
DFQ)GRW
XWZ)21Y
GPG)8SJ
KJY)GSK
RKK)VV6
ZVC)4ZL
VSJ)MJG
TBG)FD7
SHX)RW4
LF9)9LF
ZV9)KV5
8DJ)SJJ
JLP)XKV
8SV)171
SGR)WDQ
VH9)D3K
F9F)W1Q
VM9)8MH
QX4)PB2
T9F)TBG
JG4)4LT
KKD)7QN
NSL)W87
57J)VB6
WDY)7BP
GSN)2XS
FKS)1NX
QFM)PS9
KGL)VSY
5Q8)G83
D5Y)WRZ
LKR)CLT
DST)TM2
KHV)YGY
XJH)N8F
M8Y)BQ7
QHV)YWT
YK3)D5P
KV5)8F3
DLS)5BL
P5F)ZL2
RVF)GT6
4BV)97N
S8B)62C
HRL)7BF
1Y7)P9Y
WNT)36V
CVD)G1C
D1Z)3SH
PLW)2CG
Z6L)Y8G
ZZ9)GR3
G83)1WM
N38)J8Q
MJF)7JB
D1F)1Y3
8Y5)ND3
K51)YY8
1BW)Y27
GML)MNN
RF4)2Z9
4BM)T73
GS4)LFF
KTM)WFB
85D)NH8
MCD)WM4
2T5)811
8ZQ)B9S
JV1)JHZ
JDP)B7S
7DG)ZNV
G5K)Y4V
M5Y)BZC
G5N)YZ2
XQJ)7NN
54M)CDL
8TV)41S
3JL)6F6
1MP)2T5
VZV)NSP
5S4)MB3
BXT)5FJ
K9B)X5N
H79)72K
B2N)8NH
7HD)7G3
JZC)L7D
WF6)Z3T
VPW)TFP
GMV)CZG
7PB)2HK
W1Q)37S
ZM1)LMJ
14Y)36J
YZ1)CL9
ZDF)2SC
HYL)SNP
Z44)199
6ZW)C4W
7VR)5L5
13V)1FF
CDJ)JBL
YWT)GV6
H22)7N5
CXR)7HK
J6R)T5K
GR3)TX9
73H)9YN
KV7)JX3
MYM)15C
66P)MXW
7ZR)9W5
1WM)JYS
4BC)478
3FP)P7F
DHZ)6T3
GJB)Y3K
HNB)LP6
PY5)6CQ
J3W)MD9
XCH)GZ8
B5P)9PL
ZD2)WD9
SJJ)M9T
9W5)DCP
5B2)ZM4
JY6)26S
Y55)W37
J69)VN6
WM4)RNY
QBJ)QPV
J9C)GTP
8YQ)H42
W78)ZS5
8MH)C3J
MSC)7RS
HPJ)WMS
K66)R5F
7SR)5MT
8W3)28N
TQ3)9KL
2CX)HVC
PY5)76X
T9H)2SD
BNX)97Q
XK9)MZ1
PQS)DH1
1TS)ZR4
K9C)K8D
67W)L8T
CKC)BCX
LMP)7LY
NBD)XQ7
6Y9)61W
1SK)NQM
P1W)RLW
7LR)B52
K5G)T2J
R2K)D1F
7MZ)BWZ
DYR)BNQ
RPZ)M5S
6NZ)V16
ZK3)H35
J3V)HGX
2Z9)D7T
CF2)TGK
NH8)2BV
3F7)QK8
8NH)YP6
R9V)MJ9
GL2)VRF
BWX)1X5
SMH)923
N4V)ZN7
P4B)B36
537)GWD
CHF)XNK
KGY)F54
1Y3)QLD
TJ1)SGC
XS5)RQ2
NFN)RM6
343)NGP
R8L)NMV
TSY)K2H
7HN)P9J
WSW)KK3
XKZ)D5L
LF9)5WX
TY2)93B
HYB)87Q
C56)DHZ
L5G)4BC
9SP)P4G
DVP)L9C
DJ5)Z6L
KMP)SV3
H19)TDN
1B7)9WN
797)P82
6PV)WNB
DBC)DRK
JWR)J2B
MX9)SPB
FSN)BF7
SQC)NBW
WKH)CMV
PFQ)QS7
3WQ)DDB
354)F17
RLW)RF7
TGK)4WP
87T)W78
K1T)XTC
4T1)DZK
JQG)5Q8
Z6B)V82
JPL)57J
2QG)FJ4
L17)W9C
62C)YRZ
P6M)8FV
QD2)RGN
C23)3B3
8CN)ZDV
BW9)JG4
Z2T)523
FFQ)99K
FRS)RBF
MG5)7LR
6LN)QBT
K3K)QTK
WMS)S67
GH3)FB2
K6D)ZRX
TMJ)9PH
2PZ)8YQ
97Q)RGH
F8X)HBQ
5NT)H19
C4W)6YM
F81)Q62
JQR)HB5
FZB)R9B
59W)9ZK
84Y)9VY
T8Z)YPG
9PL)MDM
3TX)7G2
1X5)1GW
KP2)BX1
1M4)K51
9TS)794
26L)H5K
HH6)2FY
HWH)9X9
RW5)MLZ
P9S)GCH
HD8)6CD
RJH)PM2
YJH)4BS
46H)2WK
RLP)D3F
CDL)VTR
M2V)KFH
QX1)DX5
2X9)H6R
WS7)BCZ
2VJ)TR2
FVQ)1TS
KFH)6N4
H1F)T3D
LQ2)SJQ
79D)XCP
2RR)P7P
DS8)PYG
K84)9T5
C2Y)T7K
M9W)9H6
DRK)8QX
69G)HJJ
2ZD)4H2
BCZ)SQZ
5WX)91S
87W)BLG
XR3)9Q7
NLC)KS4
ZPQ)XFT
68Q)S8D
8RH)564
BZ9)NCK
DBS)J31
S7Y)NSH
JBW)QLF
CQS)SZP
8CK)GPG
F35)PRF
562)LJ4
7S6)2X6
8FD)C5F
TRS)CNS
DKC)1G6
9PH)4C3
HR2)1PR
JBL)ZD2
FH8)JGR
1CZ)Q2T
PS9)ZM7
FGP)P8L
VDF)G7R
RY8)644
ZXM)WYR
1SS)VCB
W37)MTQ
TQZ)NB7
9RJ)DVP
RB3)GYH
MZD)N8D
68D)LKC
MMW)CHF
LWX)ZJJ
GV9)N38
T3D)Z19
LL3)H9V
RR5)T8X
5Q8)L13
F8B)PV7
BZR)63Y
1YT)MJ5
L5Q)87T
B2J)Y4Q
K1R)9D5
59R)QFM
JTM)9RR
2SD)ZD7
1M7)3JL
NX8)FSN
LBM)M5Y
YW6)YM2
GWV)955
P31)1YT
CND)VM2
PGZ)5CM
BLG)N6R
4W3)LLC
WVJ)VL3
L88)J9D
837)8FD
XW6)NKW
GPX)GQD
858)5S1
NSH)HPJ
3B3)415
9RZ)7GZ
B28)MPM
7PM)SB1
BSS)M2V
JPX)WZ3
VL5)RRK
T51)JJ2
NLJ)FZT
9L9)R1T
5PY)8TM
9T5)8TV
DDB)XVJ
4SM)VCW
TSH)Y9L
KS4)WJC
55M)S24
CQX)DXC
8WP)P2Y
DQ4)13V
9BX)CS8
D4K)XKB
V82)DVZ
C7P)D9D
MFT)J4M
WS8)DFZ
LZ6)J3Q
FKS)TFR
78Q)LZ6
16R)FYH
GTS)DV9
H1Z)7JW
H5Z)WPW
5HD)RKH
BT2)RJ1
TXX)B6G
212)RKK
VPP)WSS
1XM)YV3
JJG)BMW
VR3)PHX
LWT)24W
CVL)D7B
923)JV1
1SK)K62
P49)P5K
7L4)QBJ
WGW)3NQ
X7C)3SP
KZX)2QG
CL9)VWR
HSN)P5F
M8M)XWS
DWY)DST
7HL)8G7
PBV)W2Y
GQP)4LX
QGJ)MH7
885)WXW
L9C)NFN
YPG)SH1
VSY)H5F
JSR)VWV
WDH)14Y
KHV)DBS
J3S)SC8
TXX)B3L
XF3)H95
4LT)GZ1
MLZ)S15
ZS9)NLH
F14)ZP3
KB5)SVJ
LP6)44K
ZL2)MML
9ZM)GMV
JQS)2GY
5J9)69X
SH1)JPP
D26)5F8
DH1)RK6
D6D)SZT
W2Y)ZSL
15K)P61
GZ8)4W3
YJ4)461
LM3)K45
R3P)TPK
HN6)GHT
W7T)GZT
JVP)43H
LP6)GWT
246)CC8
N9G)J5R
7NN)DCC
LBH)F7B
2TP)YL5
RM6)5RG
KWL)RZ4
HHB)7X4
GR4)984
M9T)HGK
M4Q)JYK
2GY)ZY8
8GG)RSR
F25)D6D
QJN)68D
NJ6)687
5KS)TYC
F83)197
9K6)XR3
YZZ)BNL
5RG)HN6
F31)2ZD
BMW)6G9
QFG)46H
JCR)LWX
HN4)VRM
6TK)Y3Z
S7J)K9B
3QZ)SD7
W59)Q2Z
99C)B2J
61W)JPX
4ZY)SHX
8XF)87N
NSP)S2S
ZTD)859
3NW)M8M
2YT)C7J
421)XJH
T73)F69
ZNV)YW6
QBJ)Y7T
V2S)MMV
ZD7)Z15
JRN)VKS
ZM7)QK3
C84)GHQ
C5D)KJB
DBS)SJ5
7HH)H88
165)R2X
XNK)FTQ
RK7)P31
84H)858
7RW)X46
XLH)VCK
DHR)DZW
GTN)TNP
HYQ)4N7
RW4)7ZF
43H)M98
X7W)5G6
6GT)839
CTR)Z9F
Q8P)GDN
8FV)6QB
L13)2RR
ZVL)KBZ
Q2Z)JLZ
BC6)WQH
MJ5)69L
4R2)G87
SFX)77Y
BWZ)KMP
STY)343
T4X)1HW
WQF)TSH
4X5)3TX
G23)JSR
DNQ)YJ4
JHZ)F14
S42)DWY
HB5)YJ6
HGG)XMD
LYY)KMJ
XYH)BXT
7BB)B7P
ZSL)G5K
G1P)L2W
MFF)HZ1
JJ2)N34
M8M)7TF
FC6)MFT
W59)QHK
5DV)QZ2
J5G)6GZ
8T3)5YQ
7QN)8Y5
1X5)SX3
DV2)H45
RXQ)4M4
L78)LG1
KG1)ZM9
8QX)QSH
MQV)RLP
HD7)B8D
82H)7VR
ZP3)DHR
CNR)LVL
WR5)9RZ
7BY)LBN
ZVC)CXR
Q9W)DQF
2BJ)TCX
JYN)HYB
64X)WNT
Y9F)QBW
BZ8)2R5
PJ1)K5T
TQN)BJD
XVJ)BB8
S2S)BWX
2ZS)K4C
VTX)P6D
KY9)TJS
QHK)5X1
3WJ)L5J
SX3)CFQ
LCB)XTM
MJP)DKB
CSR)ZML
XR3)B41
JD5)N9G
JPX)1BQ
RWL)DX1
YBG)QX1
M33)MKY
5S1)D6Y
TRS)5NK
TNP)LH6
CNS)MCR
J3Q)KWL
T4W)8W9
VJY)6WQ
JBJ)PJ1
2RR)Q8P
J4M)WQQ
69S)TJ2
NCK)16R
6H7)QF9
8Y5)J3S
77T)LBD
KB3)JRN
FNZ)1Q3
KJY)DHS
44V)GWV
NHG)TWX
VRM)BDR
YZJ)BNX
DC7)YXN
MR8)G23
MCR)BM7
HGQ)7L5
YL5)NBD
QB2)DJ5
CB1)4JP
BZC)7RG
VJZ)3BC
VWV)M33
DT7)NTG
DTK)TDB
3P8)T44
JL4)76M
7N5)GPV
ZZ4)9DX
55B)59F
D94)4ZY
5G3)M9W
DHT)SK7
TZN)2CD
QXJ)CNK
WFB)S42
7JW)1SK
X51)CYP
6DP)PBL
5BS)LF9
3NQ)DV2
Y8G)WDJ
4BW)D1Z
1GH)BC2
9H6)7GH
1XC)6FV
NZ3)59Q
8CC)NXM
9WR)7L1
DCC)498
TGQ)85Z
4H2)4T1
VSY)L7R
4ZL)SRV
PHX)2MY
T8X)XBQ
XCY)MCD
5R2)TF6
VKS)JWD
9T9)T17
8F3)XNT
DH1)NVN
VB6)P9S
7Y6)C84
3F7)3WJ
1NY)JQG
LDS)GXT
4WP)HNT
TG1)562
KJB)TQZ
D9D)T9H
XSV)C1M
X3Z)ZMX
RXV)HPL
1BQ)1M4
HNT)4MP
PBL)K84
T97)DT7
65J)CNR
7GZ)PCZ
GHT)ZDF
L7R)W9Y
NHT)HX7
SDZ)MJP
BV6)374
13H)M5L
YXN)XFS
DCP)1NY
SV3)33T
415)B34
8NL)DS8
RM5)796
G1W)RK1
5CJ)SMH
JBL)974
HPL)M3M
XNB)MB7
H9D)DQ9
4JB)6JX
XR5)B22
BMK)RR5
KD3)D7H
76X)CTC
TQD)56X
WZY)84Y
HSZ)663
SJZ)NHV
RSR)7MF
WB3)537
62Y)LHR
H69)26P
811)167
K65)7VL
Q62)3PG
YZ2)C1C
56X)VFS
6WQ)8NF
DP3)PJF
62D)NHM
T44)F8B
NDB)K1T
LVH)HFP
96D)YSP
2N9)4BM
89B)246
T3D)KH1
Y4V)2CX
6DP)TQ1
5CM)9TB
PHX)5NT
6LW)ZQQ
7LT)K1R
HBJ)9L9
XW6)NNM
BCX)KB5
8WG)ZXM";

        private class Planet
        {
            public string Name;
            public Planet parent;

            public int GetOrbits()
            {
                if (parent == null)
                    return 0;
                else return parent.GetOrbits() + 1;
            }

            public List<Planet> GetParents()
            {
                if (parent == null)
                {
                    return new List<Planet>();
                }
                var result = parent.GetParents();
                result.Add(parent);
                return result;
            }

        }

        Dictionary<string,Planet> planets = new Dictionary<string, Planet>();

        private Planet TryGet(string name)
        {
            Planet result;
            planets.TryGetValue(name, out result);
            if (result == null)
            {
                result = new Planet() { Name = name };
                planets.Add(name, result);
            }
            return result;
        }

        public int Part1()
        {
            ParseInput();

            return planets.Values.Sum(p => p.GetOrbits());

        }

        private void ParseInput()
        {
            planets.Clear();
            foreach (var line in input.Split(Environment.NewLine))
            {
                var planets = line.Split(')');
                var parent = TryGet(planets[0]);
                var child = TryGet(planets[1]);
                child.parent = parent;
            }
        }

        public int Part2()
        {
            ParseInput();

            var myparents = planets["YOU"].GetParents();
            var santaparents = planets["SAN"].GetParents();

            var inbetween = myparents.Except(santaparents).Union(santaparents.Except(myparents));

            return inbetween.Count();
        }
    }
}
