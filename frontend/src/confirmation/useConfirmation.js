import { useContext } from "react"
import { ConfirmContext } from "./ConfirmContext"


const useConfirmation = () => {
    return useContext(ConfirmContext)
};

export default useConfirmation;