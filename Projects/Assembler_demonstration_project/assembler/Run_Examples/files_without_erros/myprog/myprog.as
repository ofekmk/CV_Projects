; file myprog.as  

    mov x,y
prn #1
x: .data 1,2,3,-4,+5    ,+6
y:    .string "abbbbbccccddd"
.entry ent1
.extern ex1

;

ent1: sub x,ex1

z: cmp r1,r2

w: add r1[r2],r4

jsr r7

bne z

not w
.data -100, -200,4,+5 

lea w,z

stop
