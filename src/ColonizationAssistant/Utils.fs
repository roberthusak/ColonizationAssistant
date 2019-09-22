module ColonizationAssistant.Utils

let curry2 fn arg1 arg2 = fn (arg1, arg2)

let uncurry2 fn (arg1, arg2) = fn arg1 arg2
