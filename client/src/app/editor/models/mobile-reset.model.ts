import { Mobile } from "./mobile.model";

export class MobileReset
{
    id: number;
    mobile: Mobile;

    constructor(id: number, mob: Mobile)
    {
        this.id = id;
        this.mobile = mob;
    }
}
