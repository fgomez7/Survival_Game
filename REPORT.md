Challenges & Fixes

Enemy animations did not play correctly (running vs. death) due to missing or misconfigured Animator parameters. We fixed this by adding proper conditions (IsRunning, Die) and stopping movement logic once enemies died.

Enemies rotated or jittered when close to the player because of physics rotation and direction calculations. We resolved this by freezing Rigidbody2D rotation and adding distance checks before movement.

Enemy spawning did not trigger when the player crossed the boundary due to collider and trigger setup issues. This was fixed by enabling IsTrigger, ensuring the player had a Rigidbody2D, and correctly linking the boundary to the spawner.

Skeleton enemies spawned but did not chase the player because the player reference or Animator controller was missing on the prefab. Assigning the correct tag and Animator fixed this.

Weapon durability behaved incorrectly, causing tools to break unexpectedly. We fixed this by reducing durability only on successful hits and centralizing tool usage logic.

Axe and sword systems were duplicated, leading to bugs and extra code. We unified them using item tags and shared damage logic so multiple tools could use the same attack system.

Stone nodes did not take damage because the hitbox logic only checked for trees. We added a separate stone damage handler and ensured proper collider setup.

Dropped items flew away due to unintended physics interactions. This was fixed by adjusting Rigidbody2D settings and controlling item drop behavior.

Sprites could not be added to animations because they were not imported correctly. We fixed this by setting sprites to Sprite (2D and UI) and slicing them properly.

System integration was challenging since combat, inventory, crafting, and enemies interacted in complex ways. We solved this by organizing shared logic, using Scriptable Objects, and adding debug logs.

When the player crafted an item, the crafting buttons and resource counts did not update correctly. Sometimes an item appeared uncraftable even though the player had enough materials.The crafting UI was not refreshing after inventory changes. The inventory data updated, but the UI still displayed old valuesWe forced the inventory and crafting UI to refresh after crafting by calling refresh/update functions whenever items were added or removed. This ensured the UI always reflected the current inventory state.

Crafting recipes sometimes failed even when the correct items were in the inventory.Items were compared incorrectly (by name or new instances instead of the same ScriptableObject reference). This caused the crafting system to think the required resources were missing.We standardized item checks to compare ScriptableObject references directly, ensuring that crafted and collected items matched the recipe requirements.