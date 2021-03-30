export class Name
{
    nom: string;
    gen: string;
    dat: string;
    acc: string;
    ins: string;
    pre: string;

    constructor(name?: Name)
    {
        if (name)
        {
            this.nom = name.nom;
            this.gen = name.gen;
            this.dat = name.dat;
            this.acc = name.acc;
            this.ins = name.ins;
            this.pre = name.pre;
        }
    }
}
