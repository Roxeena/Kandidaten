#ifndef PUCK_H
#define PUCK_H

/*License*/

/************************************************************
* Name of file: Puck                                        *
* Filetype:(class, main, script, subclass, abstractclass)   *
* Subclass of the class MovingObject                        *
* Short description of class/file: Describes where the puck *
* is, calculate the puck's velocity, checks if the          *
* puck has collided with bounds, klubba or goal. If so then *
* calculate the new velocity with impulse physics.          *
* Github conflict status: Not commited                      *
************************************************************/

class Puck
{
    public:
        //Constructor
        Puck();

        //Destructor
        virtual ~Puck();
    protected:
    private:
};

#endif // PUCK_H
