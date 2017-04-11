
--print(Properties.moveSpeed)

if Properties.moveSpeed ~= nil then

    location = this.GetComponent("Location2")
    pos = location.Position

    if InputManager.GetAxis("horizontal") > 0 then
        location.Position = Vector2.__new(pos.X+Properties.moveSpeed, pos.Y)
    elseif InputManager.GetAxis("horizontal") < 0 then
        location.Position = Vector2.__new(pos.X-Properties.moveSpeed, pos.Y)
    end

else
    print("MoveSpeed Required")
end

--[[
    Things I want to add in this thing:
    -Loading scripts with specific properties like movespeed and stuff like that
        -need to decide if those properties should be stored in the script component or the entity class
        -Pros to storing in entity
            -Nicer syntax, eg. this.Properties["prop"]
                -vs. Properties["prop"]
        -Pros to storing in script
            -easy all together to implement and probably easy to use honestly

        --EH kinda got them working, good enough I guess. Not fantastic.--
    
    -More components and systems
        -Collisions
            -SAT collisions and collision boxes
            -Special collision handling
        -Text
        -Animations
        -Shape component?

    -Prefabs
    
    -Re-add TweenManager, EventManager and the other stuff
--]]