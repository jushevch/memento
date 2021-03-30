import { Item } from "./item.model";

export class ItemReset
{
    id: number;
    item: Item;

    constructor(id: number, item: Item)
    {
        this.id = id;
        this.item = item;
    }
}
