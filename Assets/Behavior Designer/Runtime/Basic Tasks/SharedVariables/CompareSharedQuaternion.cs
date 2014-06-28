using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace BehaviorDesigner.Runtime.Tasks.Basic.SharedVariables
{
    [TaskCategory("Basic/SharedVariable")]
    [TaskDescription("Returns success if the variable value is equal to the compareTo value.")]
    public class CompareSharedQuaternion : Conditional
    {
        [Tooltip("The first variable to compare")]
        public SharedQuaternion variable;
        [Tooltip("The variable to compare to")]
        public SharedQuaternion compareTo;

        public override TaskStatus OnUpdate()
        {
            return variable.Value.Equals(compareTo.Value) ? TaskStatus.Success : TaskStatus.Failure;
        }

        public override void OnReset()
        {
            if (variable != null) {
                variable.Value = Quaternion.identity;
            }
            if (compareTo != null) {
                compareTo.Value = Quaternion.identity;
            }
        }
    }
}