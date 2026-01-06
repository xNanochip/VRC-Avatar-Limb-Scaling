# VRC Avatar Limb Sclaing

Avatar Limb Scaling is a simple drag-n-drop prefab that lets you resize your VRChat avatar’s arms and legs using the in-game Expressions toggle menu. Useful to match your avatar’s proportions to your real-life body, improving Full Body Tracking.

## 🧾 Features
- Intended for any VRChat avatar
- Menu sliders to expand/shrink your Avatar's arms/legs
- Quick fix for bent knees & elbows
- Best with Full Body Tracking
- VRCFury for auto-installation
- Quest standalone/Android compatible
- Menu icons

## 🔍 How It Works
Avatar Limb Scaling is non-destructive in its default state. When the user adjusts their limbs in-game, it scales the arms and legs along the Y axis, then it invert scales the hands/feet to help alleviate any weird stretching/shrinking that can occur from non-uniform scaling.

## 📝 Dependencies & Installation
⚙️ Please have basic Unity knowledge such as uploading a VRChat avatar, importing unity packages, and familiar with VRChat Creator Companion (VCC).
1. Ensure the below dependencies are installed and up to date.
    1. In VCC, confirm you’re using the latest versions of VRChat SDK - Base and VRChat SDK - Avatars.
    2. In VCC, [Install VRCFury](https://vrcfury.com/download/) and update it if an update is available.
2. Add [Avatar Limb Scaling VPM Package](https://xnanochip.github.io/VPM-Package-Listing/) to VCC. Select "Add to VCC."
3. In VCC, add Avatar Limb Scaling to your project.
4. In Unity, Right click your avatar's object in the Hierarchy and select Nanochip > Spawn Avatar Limb Scaling Prefab.
5. (Optional) Relocate the Avatar Limb Scaling menu somewhere else in your Expressions menu.
    1. Select your Avatar’s object in the Hierarchy.
    2. In the Inspector, scroll down and click Add Component > VRCFury > Move or Rename Menu Item.
    3. For **From Path**, click Select, then choose Avatar Limb Scaling > Select this folder.
    4. For **To Path**, use Select again to choose the destination menu folder where you’d like Avatar Limb Scaling to appear.

## ⚠️ Disclaimer
When using Avatar Limb Scaling to adjust your arms or legs, your hands or feet may appear slightly smaller or larger. This is a normal tradeoff that can vary between avatars, so I recommend making only small adjustments to minimize this.

## Credits
- [Hanee_BEE](https://x.com/honeybee6519) for creating a [Unity tutorial](https://x.com/honeybee6519/status/1987192790732972330) on setting up a similar system that is found on [Googi](https://x.com/_Googii)'s avatar(s). This is what inspired me to create this drag-n-drop prefab.
- [Sacred](https://jinxxy.com/Sacred) for the wealth of knowledge, feedback, and testing.
- [BUDDYWORKS](https://buddyworks.wtf/) for creating this VPM listing for me.

## Terms of Use:
- ✅ Do whatever you want with this prefab, idc 👍
- ✅ All commercial use is allowed. Credit is appreciated but not required 👍
