import { Component, Input, Output, EventEmitter } from "@angular/core";
import { Area } from "../../models/area.model";

@Component({
    selector: "app-item-list",
    templateUrl: "./item-list.component.html"
})
export class ItemListComponent
{
    @Input() area: Area;
    @Input() isCloneDisabled: boolean;

    @Output() editItemEvent = new EventEmitter<number>();
    @Output() cloneItemEvent = new EventEmitter<number>();
    @Output() itemDeletedEvent = new EventEmitter();

    constructor() { }

    editItem(vnum: number): void
    {
        this.editItemEvent.emit(vnum);
    }

    cloneItem(vnum: number): void
    {
        if (!this.isCloneDisabled)
        {
            this.cloneItemEvent.emit(vnum);
        }
    }

    deleteItem(vnum: number): void
    {
        this.deleteBoundResets(vnum);
        this.area.items.delete(vnum);
        this.itemDeletedEvent.emit();
    }

    private deleteBoundResets(vnum: number): void
    {
        for (let room of this.area.rooms.values())
        {
            while (1)
            {
                let reset = room.itemResets.find(r => r.item.vnum == vnum);

                if (!reset)
                {
                    break;
                }

                let index = room.itemResets.indexOf(reset);
                room.itemResets.splice(index, 1);
            }
        }
    }
}
